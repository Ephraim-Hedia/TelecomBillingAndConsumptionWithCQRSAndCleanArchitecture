using Microsoft.EntityFrameworkCore;
using TelecomBillingAndConsumption.Data.Entities;
using TelecomBillingAndConsumption.Data.Helpers;
using TelecomBillingAndConsumption.Infrastructure.InfrastructureBases;
using TelecomBillingAndConsumption.Service.Interfaces;
using TelecomBillingAndConsumption.Service.Interfaces.PlanService;

namespace TelecomBillingAndConsumption.Service.Implementation
{
    public class UsageRecordService : IUsageRecordService
    {
        #region Fields
        private readonly IGenericRepository<UsageRecord> _usageRecordRepository;
        private readonly ITariffService _tariffService;
        private readonly ISubscriberService _subscriberService;
        private readonly IPlanService _planService;
        #endregion

        #region Constructors
        public UsageRecordService(
            IPlanService planService,
            ITariffService tariffService,
            IGenericRepository<UsageRecord> usageRecordRepository,
            ISubscriberService subscriberService)
        {
            _planService = planService;

            _tariffService = tariffService;
            _usageRecordRepository = usageRecordRepository;
            _subscriberService = subscriberService;
        }
        #endregion

        #region Handle Functions

        // Returns all usage records with necessary includes
        public IQueryable<UsageRecord> QueryUsageRecordsWithIncludes()
        {
            return _usageRecordRepository.GetTableNoTracking()
                .Include(r => r.Subscriber)
                .Include(r => r.BillDetails); // Add more includes if needed
        }

        // Gets all usage records
        public async Task<List<UsageRecord>> GetAllAsync()
        {
            return await QueryUsageRecordsWithIncludes().ToListAsync();
        }

        // Gets usage records by subscriber
        public async Task<List<UsageRecord>> GetBySubscriberAsync(int subscriberId)
        {
            return await QueryUsageRecordsWithIncludes()
                .Where(r => r.SubscriberId == subscriberId)
                .ToListAsync();
        }

        // Gets usage record by id
        public async Task<UsageRecord?> GetByIdAsync(int id)
        {
            return await QueryUsageRecordsWithIncludes()
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        // Create/Add
        public async Task<int> AddAsync(UsageRecord usageRecord)
        {
            // 1. Load the subscriber
            var subscriber = await _subscriberService.GetByIdAsync(usageRecord.SubscriberId);

            // 2. Set IsRoaming from subscriber table
            usageRecord.IsRoaming = subscriber.IsRoaming;

            // 3. Calculate IsPeak from timestamp
            var hour = usageRecord.Timestamp.Hour;
            usageRecord.IsPeak = hour >= 8 && hour < 20;

            // 4. Get subscriber plan via _planService
            var plan = await _planService.GetByIdAsync(subscriber.PlanId);

            // 5. Get all usage records for current month (no tracking for efficiency)
            var monthStart = new DateTime(usageRecord.Timestamp.Year, usageRecord.Timestamp.Month, 1);
            var monthEnd = monthStart.AddMonths(1);

            var usageRecordsThisMonth = await _usageRecordRepository.GetTableNoTracking()
                .Where(r => r.SubscriberId == usageRecord.SubscriberId
                         && r.Timestamp >= monthStart
                         && r.Timestamp < monthEnd)
                .ToListAsync();

            // 6. Get matching tariff
            var tariff = await _tariffService.FindTariffAsync(
                usageRecord.UsageType,
                usageRecord.IsRoaming,
                usageRecord.IsPeak);

            decimal unitPrice = tariff.PricePerUnit;

            // 7. Calculate total cost according to business rules
            switch (usageRecord.UsageType)
            {
                case UsageType.Call:
                    int alreadyUsedMin = usageRecordsThisMonth.Sum(r => r.CallMinutes ?? 0);
                    int newMin = usageRecord.CallMinutes ?? 0;
                    int bundleLimitMin = plan.IncludedCallMinutes;
                    usageRecord.TotalCost = CalculateTieredCost(alreadyUsedMin, bundleLimitMin, newMin, unitPrice);
                    usageRecord.UnitPrice = usageRecord.TotalCost / newMin;
                    break;

                case UsageType.Data:
                    decimal alreadyUsedMB = usageRecordsThisMonth.Sum(r => r.DataMB ?? 0);
                    decimal newMB = usageRecord.DataMB ?? 0;
                    decimal bundleLimitMB = plan.IncludedDataMB;
                    usageRecord.TotalCost = CalculateTieredCostDecimal(alreadyUsedMB, bundleLimitMB, newMB, unitPrice);
                    usageRecord.UnitPrice = usageRecord.TotalCost / newMB;
                    break;

                case UsageType.SMS:
                    int alreadyUsedSms = usageRecordsThisMonth.Sum(r => r.SMSCount ?? 0);
                    int newSms = usageRecord.SMSCount ?? 0;
                    int bundleLimitSms = plan.IncludedSMS;
                    usageRecord.TotalCost = CalculateTieredCost(alreadyUsedSms, bundleLimitSms, newSms, unitPrice);
                    usageRecord.UnitPrice = usageRecord.TotalCost / newSms;
                    break;

                default:
                    throw new Exception("Unknown UsageType.");
            }

            // 8. Save record
            var result = await _usageRecordRepository.AddAsync(usageRecord);
            return result.Id;
        }

        // Delete (soft delete)
        public async Task<bool> DeleteAsync(int id)
        {
            var record = await _usageRecordRepository.GetByIdAsync(id);
            if (record == null || record.IsDeleted)
                return false;
            record.IsDeleted = true;
            await _usageRecordRepository.UpdateAsync(record);
            return true;
        }

        #endregion


        #region Private Helper Functions

        private decimal CalculateTieredCost(
            int alreadyUsed, int bundleLimit, int newUnits, decimal unitPrice)
        {
            int remainingBundle = Math.Max(bundleLimit - alreadyUsed, 0);
            int normalUnits = Math.Min(newUnits, remainingBundle);
            int overageUnits = newUnits - normalUnits;
            decimal totalCost = (normalUnits * unitPrice) + (overageUnits * unitPrice * 2);
            return totalCost;
        }

        private decimal CalculateTieredCostDecimal(
            decimal alreadyUsed, decimal bundleLimit, decimal newUnits, decimal unitPrice)
        {
            decimal remainingBundle = Math.Max(bundleLimit - alreadyUsed, 0);
            decimal normalUnits = Math.Min(newUnits, remainingBundle);
            decimal overageUnits = newUnits - normalUnits;
            decimal totalCost = (normalUnits * unitPrice) + (overageUnits * unitPrice * 2);
            return totalCost;
        }

        #endregion
    }
}
