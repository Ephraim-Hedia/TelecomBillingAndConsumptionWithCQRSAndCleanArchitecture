using TelecomBillingAndConsumption.Data.Entities;

namespace TelecomBillingAndConsumption.Service.Interfaces
{
    public interface IUsageRecordService
    {
        IQueryable<UsageRecord> QueryUsageRecordsWithIncludes();
        Task<List<UsageRecord>> GetAllAsync();
        Task<List<UsageRecord>> GetBySubscriberAsync(int subscriberId);
        Task<UsageRecord?> GetByIdAsync(int id);
        Task<int> AddAsync(UsageRecord usageRecord);
        Task<bool> DeleteAsync(int id);

    }
}
