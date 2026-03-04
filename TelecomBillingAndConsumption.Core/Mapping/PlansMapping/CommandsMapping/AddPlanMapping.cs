using TelecomBillingAndConsumption.Core.Features.PlansFeatures.Commands.Models;
using TelecomBillingAndConsumption.Data.Entities;

namespace TelecomBillingAndConsumption.Core.Mapping.PlansMapping
{
    public partial class PlansProfile
    {
        public void AddPlanMapping()
        {
            CreateMap<AddPlanCommand, Plan>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.RowVersion, opt => opt.Ignore());
        }
    }
}
