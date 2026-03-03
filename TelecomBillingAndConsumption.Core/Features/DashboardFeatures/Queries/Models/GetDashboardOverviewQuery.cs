using MediatR;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.DashboardFeatures.Queries.Results;

namespace TelecomBillingAndConsumption.Core.Features.DashboardFeatures.Queries.Models
{
    public class GetDashboardOverviewQuery : IRequest<Response<GetDashboardOverviewResponse>>
    {
    }
}
