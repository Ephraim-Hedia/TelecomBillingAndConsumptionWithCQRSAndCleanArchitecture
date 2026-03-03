using MediatR;
using TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Queries.Results;

namespace TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Queries.Models
{
    public class GetSubscriberByPhoneNumberQuery : IRequest<GetSubscriberByPhoneNumberResponse>
    {
        public string PhoneNumber { get; set; }
    }
}
