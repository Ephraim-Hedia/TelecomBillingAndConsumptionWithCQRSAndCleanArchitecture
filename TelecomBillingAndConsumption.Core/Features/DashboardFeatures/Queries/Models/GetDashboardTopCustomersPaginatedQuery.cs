using MediatR;
using TelecomBillingAndConsumption.Core.Features.DashboardFeatures.Queries.Results;

namespace TelecomBillingAndConsumption.Core.Features.DashboardFeatures.Queries.Models
{
    public class GetDashboardTopCustomersPaginatedQuery : IRequest<List<GetDashboardTopCustomersPaginatedResponse>>
    {
        public int Limit { get; set; } = 10;
    }
}
