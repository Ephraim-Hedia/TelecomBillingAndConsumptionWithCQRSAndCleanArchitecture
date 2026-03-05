using MediatR;
using TelecomBillingAndConsumption.Core.Bases;

namespace TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Commands.Models
{
    public class UpdateSubscriberPlanCommand : IRequest<Response<string>>
    {
        public int SubscriberId { get; set; }
        public int NewPlanId { get; set; }
    }
}
