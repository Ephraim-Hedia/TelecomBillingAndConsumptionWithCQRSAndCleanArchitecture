namespace TelecomBillingAndConsumption.Service.Interfaces
{
    public interface IPlanLimitService
    {
        Task<bool> IsCallLimitExceededAsync(int subscriberId, DateTime usageTimestamp, int newCallMinutes);
        Task<bool> IsDataLimitExceededAsync(int subscriberId, DateTime usageTimestamp, decimal newDataMB);
        Task<bool> IsSmsLimitExceededAsync(int subscriberId, DateTime usageTimestamp, int newSmsCount);
    }
}
