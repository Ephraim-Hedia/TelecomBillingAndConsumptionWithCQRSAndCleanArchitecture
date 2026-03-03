using MediatR;
using TelecomBillingAndConsumption.Core.Bases;

namespace TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Commands.Models
{
    public class DeleteSubscriberByIdCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteSubscriberByIdCommand(int id)
        {
            Id = id;
        }
    }
}
