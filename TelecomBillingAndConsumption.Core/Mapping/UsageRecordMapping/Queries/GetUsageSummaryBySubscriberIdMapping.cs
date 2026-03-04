using TelecomBillingAndConsumption.Core.Features.UsageFeatures.Queries.Models;
using TelecomBillingAndConsumption.Data.Entities;

namespace TelecomBillingAndConsumption.Core.Mapping.UsageRecordMapping
{
    public partial class UsageRecordProfile
    {
        public void GetUsageSummaryBySubscriberIdMapping()
        {
            CreateMap<GetUsageSummaryBySubscriberIdQuery, UsageRecord>().ReverseMap();
        }
    }
}
