using MediatR;
using TelecomBillingAndConsumption.Core.Bases;

namespace TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Commands.Models
{
    public class DeactivateSubscriberByIdCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeactivateSubscriberByIdCommand(int id)
        {
            Id = id;
        }
    }
}
