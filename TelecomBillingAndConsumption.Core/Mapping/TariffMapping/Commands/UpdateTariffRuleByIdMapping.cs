using TelecomBillingAndConsumption.Core.Features.TariffsFeatures.Commands.Models;
using TelecomBillingAndConsumption.Data.Entities;

namespace TelecomBillingAndConsumption.Core.Mapping.TariffMapping
{
    public partial class TariffProfile
    {
        public void UpdateTariffRuleByIdCommandToTariffRule()
        {
            CreateMap<UpdateTariffRuleByIdCommand, TariffRule>().ReverseMap();
        }
    }
}
