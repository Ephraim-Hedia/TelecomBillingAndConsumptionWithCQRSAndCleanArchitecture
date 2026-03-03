namespace TelecomBillingAndConsumption.Data.Entities
{
    public class Bill : BaseEntity
    {
        public int SubscriberId { get; set; }

        public int BillingMonth { get; set; }

        public int BillingYear { get; set; }

        public decimal PlanFee { get; set; }

        public decimal UsageCost { get; set; }

        public decimal RoamingSurcharge { get; set; }

        public decimal ExtraUsageCost { get; set; }

        public decimal VatAmount { get; set; }

        public decimal LoyaltyDiscount { get; set; }

        public decimal TotalAmount { get; set; }

        public bool IsPaid { get; set; }

        // Navigation
        public Subscriber Subscriber { get; set; } = null!;

        public ICollection<BillDetail> BillDetails { get; set; } = new HashSet<BillDetail>();

        public ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
    }
}
