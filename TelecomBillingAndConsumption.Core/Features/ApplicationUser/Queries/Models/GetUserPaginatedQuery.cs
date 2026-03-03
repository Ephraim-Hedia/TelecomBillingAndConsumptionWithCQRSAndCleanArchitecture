using MediatR;
using TelecomBillingAndConsumption.Core.Features.ApplicationUser.Queries.Results;
using TelecomBillingAndConsumption.Core.Wrappers;

namespace TelecomBillingAndConsumption.Core.Features.ApplicationUser.Queries.Models
{

    public class GetUserPaginatedQuery : IRequest<PaginatedResult<GetUserPaginatedResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
