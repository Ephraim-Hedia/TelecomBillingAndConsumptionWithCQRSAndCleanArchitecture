using MediatR;
using TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Results;

namespace TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Models
{
    public class GetAllBillingsBySubscriberIdQuery : IRequest<List<GetAllBillingsBySubscriberIdResponse>>
    {

    }
}
