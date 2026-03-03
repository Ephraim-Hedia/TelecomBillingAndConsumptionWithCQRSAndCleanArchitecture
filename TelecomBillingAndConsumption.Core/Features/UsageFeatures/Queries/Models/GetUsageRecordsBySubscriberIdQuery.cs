using MediatR;
using TelecomBillingAndConsumption.Core.Features.UsageFeatures.Queries.Results;
using TelecomBillingAndConsumption.Core.Wrappers;

namespace TelecomBillingAndConsumption.Core.Features.UsageFeatures.Queries.Models
{
    public class GetUsageRecordsBySubscriberIdQuery : IRequest<PaginatedResult<GetUsageRecordsBySubscriberIdResponse>>
    {
        public int SubscriberId { get; set; }
    }
}
