using TelecomBillingAndConsumption.Data.Helpers;

namespace TelecomBillingAndConsumption.Core.Features.TariffsFeatures.Queries.Results
{
    public class GetAllTariffsRulesResponse
    {
        public int Id { get; set; }

        public UsageType UsageType { get; set; }

        public bool IsRoaming { get; set; }

        public bool IsPeak { get; set; }

        public decimal PricePerUnit { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }
    }
}
