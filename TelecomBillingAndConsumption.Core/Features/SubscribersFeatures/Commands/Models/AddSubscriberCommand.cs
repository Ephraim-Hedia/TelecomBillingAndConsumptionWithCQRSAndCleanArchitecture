using MediatR;
using TelecomBillingAndConsumption.Core.Bases;

namespace TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Commands.Models
{
    public class AddSubscriberCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Country { get; set; }

        public int PlanId { get; set; }

        public bool IsRoaming { get; set; }
    }
}
