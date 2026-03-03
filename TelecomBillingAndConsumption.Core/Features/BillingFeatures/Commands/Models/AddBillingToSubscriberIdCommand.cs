using MediatR;
using TelecomBillingAndConsumption.Core.Bases;

namespace TelecomBillingAndConsumption.Core.Features.BillingFeatures.Commands.Models
{
    public class AddBillingToSubscriberIdCommand : IRequest<Response<int>>
    {
        public int SubscriberId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }
    }
}
