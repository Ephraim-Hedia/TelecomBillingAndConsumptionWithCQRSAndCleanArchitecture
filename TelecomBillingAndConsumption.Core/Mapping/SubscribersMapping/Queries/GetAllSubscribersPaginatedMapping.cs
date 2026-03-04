using TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Queries.Results;
using TelecomBillingAndConsumption.Data.Entities;

namespace TelecomBillingAndConsumption.Core.Mapping.SubscribersMapping
{
    public partial class SubscribersProfile
    {
        public void GetAllSubscribersPaginatedMapping()
        {
            CreateMap<GetAllSubscribersPaginatedResponse, Subscriber>().ReverseMap();
        }

    }
}
