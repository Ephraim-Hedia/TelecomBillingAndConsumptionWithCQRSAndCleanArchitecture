using AutoMapper;

namespace TelecomBillingAndConsumption.Core.Mapping.UsageRecordMapping
{
    public partial class UsageRecordProfile : Profile
    {
        public UsageRecordProfile()
        {
            AddUsageRecordMapping();
            GetUsageByIdMapping();
            GetUsageRecordsBySubscriberIdMapping();
            GetUsageSummaryBySubscriberIdMapping();
        }
    }
}
