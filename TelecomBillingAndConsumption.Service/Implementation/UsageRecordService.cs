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
        private readonly ISubscriberService _subscriberService;
        private const int PeakStart = 8;
        private const int PeakEnd = 20;
        #endregion

        #region Constructors
        public UsageRecordService(
            IGenericRepository<UsageRecord> usageRecordRepository,
            ISubscriberService subscriberService)
        {
            _usageRecordRepository = usageRecordRepository;
            _subscriberService = subscriberService;
        }
        #endregion

        #region Handle Functions


        // Create/Add
        public async Task<int> AddAsync(UsageRecord usageRecord)
        {
            ValidateUsage(usageRecord);

            // 1. Load the subscriber
            var subscriber = await _subscriberService.GetByIdAsync(usageRecord.SubscriberId);
            if (subscriber == null)
                throw new KeyNotFoundException("Subscriber not found.");


            // 2. Set IsRoaming from subscriber table
            usageRecord.IsRoaming = subscriber.IsRoaming;

            // 3. Calculate IsPeak from timestamp
            if (usageRecord.Timestamp == default)
                throw new ArgumentException("Timestamp is required.");

            if (usageRecord.Timestamp > DateTime.UtcNow)
                throw new ArgumentException("Usage timestamp cannot be in the future.");

            usageRecord.IsPeak = IsPeakHour(usageRecord.Timestamp);

            var result = await _usageRecordRepository.AddAsync(usageRecord);
            return result.Id;
        }


        // Returns all usage records with necessary includes
        public IQueryable<UsageRecord> QueryUsageRecordsWithIncludes()
        {
            return _usageRecordRepository
                        .GetTableNoTracking()
                        .Where(x => !x.IsDeleted)
                        .Include(r => r.Subscriber);
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
        private bool IsPeakHour(DateTime timestamp)
        {
            var hour = timestamp.Hour;
            return hour >= PeakStart && hour < PeakEnd;
        }
        private void ValidateUsage(UsageRecord record)
        {
            switch (record.UsageType)
            {
                case UsageType.Call:
                    if (record.CallMinutes == null || record.CallMinutes <= 0)
                        throw new ArgumentException("Call minutes must be greater than zero.");
                    break;

                case UsageType.Data:
                    if (record.DataMB == null || record.DataMB <= 0)
                        throw new ArgumentException("Data usage must be greater than zero.");
                    break;

                case UsageType.SMS:
                    if (record.SMSCount == null || record.SMSCount <= 0)
                        throw new ArgumentException("SMS count must be greater than zero.");
                    break;
            }
        }
        #endregion
    }
}
