using MediatR;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.PlansFeatures.Queries.Results;

namespace TelecomBillingAndConsumption.Core.Features.PlansFeatures.Queries.Models
{
    public class GetPlanByIdQuery : IRequest<Response<GetPlanByIdResponse>>
    {
        public int Id { get; set; }
        public GetPlanByIdQuery(int id)
        {
            Id = id;
        }
    }
}
