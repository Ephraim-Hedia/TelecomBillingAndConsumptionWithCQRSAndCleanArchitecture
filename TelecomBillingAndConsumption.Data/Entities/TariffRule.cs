using TelecomBillingAndConsumption.Data.Helpers;

namespace TelecomBillingAndConsumption.Data.Entities
{
    public class TariffRule : BaseEntity
    {
        public UsageType UsageType { get; set; }

        public bool IsRoaming { get; set; }

        public bool IsPeak { get; set; }

        public decimal PricePerUnit { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }
    }
}
