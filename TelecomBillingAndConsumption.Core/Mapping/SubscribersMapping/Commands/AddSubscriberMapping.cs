using TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Commands.Models;
using TelecomBillingAndConsumption.Data.Entities;

namespace TelecomBillingAndConsumption.Core.Mapping.SubscribersMapping
{
    public partial class SubscribersProfile
    {
        public void AddSubscriberMapping()
        {
            CreateMap<AddSubscriberCommand, Subscriber>().ReverseMap();
        }
    }
}
