using MediatR;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.UsageFeatures.Queries.Results;

namespace TelecomBillingAndConsumption.Core.Features.UsageFeatures.Queries.Models
{
    public class GetUsageRecordByIdQuery : IRequest<Response<GetUsageRecordByIdResponse>>
    {
        public int Id { get; set; }
    }
}
