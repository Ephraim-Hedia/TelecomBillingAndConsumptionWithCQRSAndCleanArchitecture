using TelecomBillingAndConsumption.Core.Features.UsageFeatures.Queries.Results;
using TelecomBillingAndConsumption.Data.Entities;

namespace TelecomBillingAndConsumption.Core.Mapping.UsageRecordMapping
{
    public partial class UsageRecordProfile
    {
        public void GetUsageRecordByIdMapping()
        {
            CreateMap<UsageRecord, GetUsageRecordByIdResponse>()
                .ForMember(dest => dest.SubscriberPhone,
                    opt => opt.MapFrom(src => src.Subscriber.PhoneNumber));
        }
    }
}
