using MediatR;
using TelecomBillingAndConsumption.Core.Bases;

namespace TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Commands.Models
{
    public class ActivateUserByIdCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public ActivateUserByIdCommand(int id)
        {
            Id = id;
        }
    }
}
