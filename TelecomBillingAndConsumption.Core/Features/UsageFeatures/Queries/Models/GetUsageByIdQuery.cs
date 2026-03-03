using MediatR;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.UsageFeatures.Queries.Results;

namespace TelecomBillingAndConsumption.Core.Features.UsageFeatures.Queries.Models
{
    public class GetUsageByIdQuery : IRequest<Response<GetUsageByIdResponse>>
    {
        public int Id { get; set; }
    }
}
