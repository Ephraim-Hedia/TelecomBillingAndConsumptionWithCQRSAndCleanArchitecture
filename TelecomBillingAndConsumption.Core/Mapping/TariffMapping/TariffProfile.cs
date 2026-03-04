using AutoMapper;

namespace TelecomBillingAndConsumption.Core.Mapping.TariffMapping
{
    public partial class TariffProfile : Profile
    {
        public TariffProfile()
        {
            AddTariffRuleMapping();
            UpdateTariffRuleByIdCommandToTariffRule();

            GetAllTariffRulesMapping();
            GetTariffRuleByIdMapping();
        }
    }
}
