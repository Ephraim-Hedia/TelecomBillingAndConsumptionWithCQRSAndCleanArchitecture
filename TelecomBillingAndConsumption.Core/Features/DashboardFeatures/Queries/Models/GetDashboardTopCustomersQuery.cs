using MediatR;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.DashboardFeatures.Queries.Results;

namespace TelecomBillingAndConsumption.Core.Features.DashboardFeatures.Queries.Models
{
    public class GetDashboardTopCustomersQuery : IRequest<Response<List<GetDashboardTopCustomersResponse>>>
    {
        public int TopN { get; set; }
    }
}
