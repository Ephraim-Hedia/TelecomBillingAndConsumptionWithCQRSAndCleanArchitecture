using Microsoft.EntityFrameworkCore;
using TelecomBillingAndConsumption.Data.Entities;
using TelecomBillingAndConsumption.Data.Helpers;
using TelecomBillingAndConsumption.Infrastructure.InfrastructureBases;
using TelecomBillingAndConsumption.Service.Interfaces;

namespace TelecomBillingAndConsumption.Service.Implementation
{
    public class UsageRecordService : IUsageRecordService
    {
        #region Fields
        private readonly IGenericRepository<UsageRecord> _usageRecordRepository;
        private readonly ITariffService _tariffService;
        #endregion

        #region Constructors
        public UsageRecordService(
            ITariffService tariffService,
            IGenericRepository<UsageRecord> usageRecordRepository)
        {
            _tariffService = tariffService;
            _usageRecordRepository = usageRecordRepository;
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

            // At this point the validator has ALREADY checked:
            // - Subscriber exists
            // - Tariff exists for the (UsageType, IsRoaming, IsPeak)

            // 1. Get the needed tariff to calculate pricing; assume always found
            var tariff = await _tariffService.FindTariffAsync(
                usageRecord.UsageType,
                usageRecord.IsRoaming,
                usageRecord.IsPeak);

            // 2. Fill derived fields
            usageRecord.UnitPrice = tariff.PricePerUnit;

            usageRecord.TotalCost = usageRecord.UsageType switch
            {
                UsageType.Call => (usageRecord.CallMinutes ?? 0) * usageRecord.UnitPrice,
                UsageType.Data => (usageRecord.DataMB ?? 0) * usageRecord.UnitPrice,
                UsageType.SMS => (usageRecord.SMSCount ?? 0) * usageRecord.UnitPrice,
                _ => throw new Exception("Unknown UsageType.")
            };

            // 3. Save to DB
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
    }
}
