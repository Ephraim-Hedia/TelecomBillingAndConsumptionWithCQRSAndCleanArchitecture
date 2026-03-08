using AutoMapper;

namespace TelecomBillingAndConsumption.Core.Mapping.UsageRecordMapping
{
    public partial class UsageRecordProfile : Profile
    {
        public UsageRecordProfile()
        {
            AddUsageRecordMapping();
            GetUsageRecordByIdMapping();
            GetUsageRecordsBySubscriberIdMapping();
        }
    }
}
