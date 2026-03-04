using TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Commands.Models;
using TelecomBillingAndConsumption.Data.Entities;

namespace TelecomBillingAndConsumption.Core.Mapping.SubscribersMapping
{
    public partial class SubscribersProfile
    {
        public void UpdateSubscriberByIdMapping()
        {
            CreateMap<UpdateSubscriberByIdCommand, Subscriber>().ReverseMap();
        }
    }
}
