using MediatR;
using TelecomBillingAndConsumption.Core.Features.UsageFeatures.Queries.Results;
using TelecomBillingAndConsumption.Core.Wrappers;

namespace TelecomBillingAndConsumption.Core.Features.UsageFeatures.Queries.Models
{
    public class GetUsageSummaryBySubscriberIdQuery : IRequest<PaginatedResult<GetUsageSummaryBySubscriberIdResponse>>
    {
        public int SubscriberId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }
    }
}
