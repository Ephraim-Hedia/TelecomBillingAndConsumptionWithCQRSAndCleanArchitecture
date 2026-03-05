using MediatR;
using TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Results;
using TelecomBillingAndConsumption.Core.Wrappers;

namespace TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Models
{
    public class GetAllBillingsBySubscriberIdQuery : IRequest<PaginatedResult<GetAllBillingsBySubscriberIdResponse>>
    {
        public int SubscriberId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
