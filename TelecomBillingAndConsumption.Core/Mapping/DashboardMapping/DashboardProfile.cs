using AutoMapper;

namespace TelecomBillingAndConsumption.Core.Mapping.DashboardMapping
{
    public partial class DashboardProfile : Profile
    {
        public DashboardProfile()
        {
            GetDashboardTopCustomersMapping();
        }
    }
}
