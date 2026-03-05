using MediatR;
using TelecomBillingAndConsumption.Core.Bases;

namespace TelecomBillingAndConsumption.Core.Features.BillingFeatures.Commands.Models
{
    public class AddBillingToSubscriberIdCommand : IRequest<Response<int>>
    {
        public int SubscriberId { get; set; }

        public string Month { get; set; }

    }
}
