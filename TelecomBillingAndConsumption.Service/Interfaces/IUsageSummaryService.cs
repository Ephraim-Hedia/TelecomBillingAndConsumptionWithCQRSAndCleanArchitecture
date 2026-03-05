using TelecomBillingAndConsumption.Service.HelperDtos;

namespace TelecomBillingAndConsumption.Service.Interfaces
{
    public interface IUsageSummaryService
    {
        Task<SubscriberUsageSummaryResponse> GetUsageSummaryAsync(int subscriberId, DateTime periodStart, DateTime periodEnd);
    }
}
