using MediatR;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Results;

namespace TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Models
{
    public class GetBillByIdQuery : IRequest<Response<GetBillByIdResponse>>
    {

    }
}
