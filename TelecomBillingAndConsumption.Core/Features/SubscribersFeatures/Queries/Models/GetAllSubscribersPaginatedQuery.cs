using MediatR;
using TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Queries.Results;
using TelecomBillingAndConsumption.Core.Wrappers;

namespace TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Queries.Models
{
    public class GetAllSubscribersPaginatedQuery : IRequest<PaginatedResult<GetAllSubscribersPaginatedResponse>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
