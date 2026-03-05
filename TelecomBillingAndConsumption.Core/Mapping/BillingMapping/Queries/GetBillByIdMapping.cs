using TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Results;
using TelecomBillingAndConsumption.Data.Entities;

namespace TelecomBillingAndConsumption.Core.Mapping.BillingMapping
{
    public partial class BillingProfile
    {
        public void GetBillByIdMapping()
        {

            CreateMap<Bill, GetBillByIdResponse>()
                .ForMember(dest => dest.BillDetails, opt => opt.MapFrom(src => src.BillDetail))
                .ForMember(dest => dest.BillId, opt => opt.MapFrom(src => src.Id));

            CreateMap<BillDetail, BillDetailsResponse>();
        }
    }
}
