using MediatR;
using TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Results;

namespace TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Models
{
    public class GetBillingDetailsByBillIdQuery : IRequest<List<GetBillingDetailsByBillIdResponse>>
    {
        public int BillId { get; set; }
    }
}
