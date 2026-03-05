using AutoMapper;

namespace TelecomBillingAndConsumption.Core.Mapping.BillingMapping
{
    public partial class BillingProfile : Profile
    {
        public BillingProfile()
        {
            GetBillByIdMapping();
            GetAllBillingsBySubscriberIdMapping();
            GetBillBySubscriberIdAndMonthMapping();
        }
    }
}
