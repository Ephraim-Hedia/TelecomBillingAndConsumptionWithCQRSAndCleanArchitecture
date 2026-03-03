using TelecomBillingAndConsumption.Data.Helpers;

namespace TelecomBillingAndConsumption.Data.Entities
{
    public class UsageRecord : BaseEntity
    {
        public int SubscriberId { get; set; }

        public UsageType UsageType { get; set; }

        public int? CallMinutes { get; set; }

        public decimal? DataMB { get; set; }

        public int? SMSCount { get; set; }

        public bool IsRoaming { get; set; }

        public bool IsPeak { get; set; }

        public DateTime Timestamp { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalCost { get; set; }

        // Navigation
        public Subscriber Subscriber { get; set; } = null!;

        public ICollection<BillDetail> BillDetails { get; set; } = new HashSet<BillDetail>();
    }
}
