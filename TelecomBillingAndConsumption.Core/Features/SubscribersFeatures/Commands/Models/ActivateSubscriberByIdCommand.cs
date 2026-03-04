using MediatR;
using TelecomBillingAndConsumption.Core.Bases;

namespace TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Commands.Models
{
    public class ActivateSubscriberByIdCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public ActivateSubscriberByIdCommand(int id)
        {
            Id = id;
        }
    }
}
