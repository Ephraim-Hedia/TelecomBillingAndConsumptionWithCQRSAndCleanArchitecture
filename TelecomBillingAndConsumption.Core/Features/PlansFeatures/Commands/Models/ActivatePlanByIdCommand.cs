using MediatR;
using TelecomBillingAndConsumption.Core.Bases;

namespace TelecomBillingAndConsumption.Core.Features.PlansFeatures.Commands.Models
{
    public class ActivatePlanByIdCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public ActivatePlanByIdCommand(int id) => Id = id;
    }
}
