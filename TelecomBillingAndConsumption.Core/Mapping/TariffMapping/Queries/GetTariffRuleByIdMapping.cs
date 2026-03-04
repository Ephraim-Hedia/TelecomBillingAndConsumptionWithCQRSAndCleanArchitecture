using TelecomBillingAndConsumption.Core.Features.TariffsFeatures.Queries.Results;
using TelecomBillingAndConsumption.Data.Entities;

namespace TelecomBillingAndConsumption.Core.Mapping.TariffMapping
{
    public partial class TariffProfile
    {
        public void GetTariffRuleByIdMapping()
        {
            CreateMap<TariffRule, GetTariffRuleByIdResponse>().ReverseMap();
        }
    }
}
