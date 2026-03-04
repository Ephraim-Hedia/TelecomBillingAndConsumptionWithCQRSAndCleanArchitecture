using MediatR;
using TelecomBillingAndConsumption.Core.Bases;

namespace TelecomBillingAndConsumption.Core.Features.PlansFeatures.Commands.Models
{
    public class DeactivatePlanByIdCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeactivatePlanByIdCommand(int id) => Id = id;
    }
}
