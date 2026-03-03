using MediatR;
using TelecomBillingAndConsumption.Core.Features.PlansFeatures.Queries.Results;
using TelecomBillingAndConsumption.Core.Wrappers;

namespace TelecomBillingAndConsumption.Core.Features.PlansFeatures.Queries.Models
{
    public class GetAllPlansPaginatedQuery : IRequest<PaginatedResult<GetAllPlansPaginatedResponse>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
