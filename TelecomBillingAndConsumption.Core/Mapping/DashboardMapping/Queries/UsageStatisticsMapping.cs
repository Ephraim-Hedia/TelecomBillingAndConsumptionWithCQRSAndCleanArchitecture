using TelecomBillingAndConsumption.Core.Features.DashboardFeatures.Queries.Results;
using TelecomBillingAndConsumption.Service.HelperDtos;

namespace TelecomBillingAndConsumption.Core.Mapping.DashboardMapping
{
    public partial class DashboardProfile
    {
        public void UsageStatisticsMapping()
        {
            CreateMap<UsageStatistics, UsageStatisticsResponse>();
        }
    }
}
