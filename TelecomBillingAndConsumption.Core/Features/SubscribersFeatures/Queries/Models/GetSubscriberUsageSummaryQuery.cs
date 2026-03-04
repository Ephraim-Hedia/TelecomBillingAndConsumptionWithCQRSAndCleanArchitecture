using MediatR;
using TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Queries.Results;

namespace TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Queries.Models
{
    public class GetSubscriberUsageSummaryQuery : IRequest<SubscriberUsageSummaryResponse>
    {
        public int SubscriberId { get; set; }
        public GetSubscriberUsageSummaryQuery(int id) => SubscriberId = id;

    }
}
