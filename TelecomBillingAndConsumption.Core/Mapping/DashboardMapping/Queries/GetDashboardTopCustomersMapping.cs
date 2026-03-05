using TelecomBillingAndConsumption.Core.Features.DashboardFeatures.Queries.Results;
using TelecomBillingAndConsumption.Service.HelperDtos;

namespace TelecomBillingAndConsumption.Core.Mapping.DashboardMapping
{
    public partial class DashboardProfile
    {
        public void GetDashboardTopCustomersMapping()
        {
            CreateMap<TopCustomerDto, GetDashboardTopCustomersResponse>()
                .ForMember(dest => dest.TotalBillingAmount, opt => opt.MapFrom(src => src.TotalCost));
        }
    }
}
