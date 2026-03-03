using MediatR;
using TelecomBillingAndConsumption.Core.Bases;

namespace TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Commands.Models
{
    public class DeactivateUserByIdCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeactivateUserByIdCommand(int id)
        {
            Id = id;
        }
    }
}
