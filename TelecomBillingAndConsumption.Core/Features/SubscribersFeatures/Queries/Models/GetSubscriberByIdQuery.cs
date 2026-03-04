using MediatR;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Queries.Results;

namespace TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Queries.Models
{
    public class GetSubscriberByIdQuery : IRequest<Response<GetSubscriberByIdResponse>>
    {
        public int Id { get; set; }
        public GetSubscriberByIdQuery(int id) => Id = id;

    }
}
