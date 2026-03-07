using AutoMapper;

namespace TelecomBillingAndConsumption.Core.Mapping.SubscribersMapping
{
    public partial class SubscribersProfile : Profile
    {
        public SubscribersProfile()
        {
            AddSubscriberMapping();
            UpdateSubscriberByIdMapping();
            GetAllSubscribersPaginatedMapping();
            GetSubscriberByIdMapping();
            GetSubscriberByPhoneNumberMappingMethod();
            GetSubscriberUsageSummaryMapping();
        }
    }
}
