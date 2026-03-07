using TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Queries.Results;

namespace TelecomBillingAndConsumption.Core.Mapping.SubscribersMapping
{
    public partial class SubscribersProfile
    {
        public void GetSubscriberUsageSummaryMapping()
        {
            CreateMap<SubscriberUsageSummaryResponse, Service.HelperDtos.SubscriberUsageSummaryResponse>().ReverseMap();
        }
    }
}
