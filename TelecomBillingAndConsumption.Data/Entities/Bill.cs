namespace TelecomBillingAndConsumption.Data.Entities
{
    public class Bill : BaseEntity
    {
        public int SubscriberId { get; set; }

        public string? Month { get; set; } // Format "YYYY-MM"

        public decimal? PlanFee { get; set; }

        public decimal? UsageCost { get; set; }

        public decimal? RoamingSurcharge { get; set; }

        public decimal? ExtraUsageCost { get; set; }

        public decimal? LoyaltyDiscount { get; set; }

        public decimal? VatAmount { get; set; }

        public decimal? TotalAmount { get; set; }

        public bool IsPaid { get; set; } = false;

        // Navigation
        public Subscriber? Subscriber { get; set; } = null!;

        public BillDetail? BillDetail { get; set; }

        //public Payment? Payment { get; set; } = null!;
    }
}
