using TelecomBillingAndConsumption.Core.Features.PlansFeatures.Commands.Models;
using TelecomBillingAndConsumption.Data.Entities;

namespace TelecomBillingAndConsumption.Core.Mapping.PlansMapping
{
    public partial class PlansProfile
    {
        public void UpdatePlanByIdMapping()
        {
            CreateMap<UpdatePlanByIdCommand, Plan>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
