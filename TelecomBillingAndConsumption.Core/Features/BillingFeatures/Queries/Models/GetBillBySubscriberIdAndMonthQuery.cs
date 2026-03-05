
using MediatR;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Results;

namespace TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Models
{
    public class GetBillBySubscriberIdAndMonthQuery : IRequest<Response<GetBillBySubscriberIdAndMonthResponse>>
    {
        public int SubscriberId { get; set; }
        public string Month { get; set; }
    }
}
