using MediatR;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Queries.Results;

namespace TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Queries.Models
{
    public class GetSubscriberUsageSummaryQuery : IRequest<Response<SubscriberUsageSummaryResponse>>
    {
        public int SubscriberId { get; set; }
        public GetSubscriberUsageSummaryQuery(int id) => SubscriberId = id;

    }
}
