using TelecomBillingAndConsumption.Core.Features.PlansFeatures.Queries.Results;
using TelecomBillingAndConsumption.Data.Entities;

namespace TelecomBillingAndConsumption.Core.Mapping.PlansMapping
{
    public partial class PlansProfile
    {
        public void GetPlanByIdMapping()
        {
            CreateMap<Plan, GetPlanByIdResponse>();
        }
    }
}
