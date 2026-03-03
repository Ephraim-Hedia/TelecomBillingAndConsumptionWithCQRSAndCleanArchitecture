using MediatR;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.DashboardFeatures.Queries.Results;

namespace TelecomBillingAndConsumption.Core.Features.DashboardFeatures.Queries.Models
{
    public class GetUsageStatisticsQuery : IRequest<Response<UsageStatisticsResponse>>
    {
        public int Month { get; set; }

        public int Year { get; set; }
    }
}
