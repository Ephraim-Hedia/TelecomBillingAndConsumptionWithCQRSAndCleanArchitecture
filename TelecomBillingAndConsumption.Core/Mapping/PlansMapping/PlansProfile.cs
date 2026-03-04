using AutoMapper;

namespace TelecomBillingAndConsumption.Core.Mapping.PlansMapping
{
    public partial class PlansProfile : Profile
    {
        public PlansProfile()
        {
            AddPlanMapping();
            UpdatePlanByIdMapping();
            GetAllPlansPaginatedMapping();
            GetPlanByIdMapping();
        }
    }
}
