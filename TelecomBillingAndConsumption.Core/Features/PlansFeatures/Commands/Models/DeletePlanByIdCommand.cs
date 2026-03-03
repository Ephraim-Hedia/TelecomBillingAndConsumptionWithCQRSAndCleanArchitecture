using MediatR;
using TelecomBillingAndConsumption.Core.Bases;

namespace TelecomBillingAndConsumption.Core.Features.PlansFeatures.Commands.Models
{
    public class DeletePlanByIdCommand : IRequest<Response<string>> // "Plan Deleted Successfully"
    {
        public int Id { get; set; }
        public DeletePlanByIdCommand(int id)
        {
            Id = id;
        }
    }
}
