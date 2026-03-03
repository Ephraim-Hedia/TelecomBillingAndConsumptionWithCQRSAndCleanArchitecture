namespace TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Results
{
    public class GetBillByIdResponse
    {
        public int BillId { get; set; }

        public int SubscriberId { get; set; }

        public decimal PlanFee { get; set; }

        public decimal UsageCost { get; set; }

        public decimal RoamingSurcharge { get; set; }

        public decimal ExtraUsageCost { get; set; }

        public decimal VatAmount { get; set; }

        public decimal LoyaltyDiscount { get; set; }

        public decimal TotalAmount { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public bool IsPaid { get; set; }
    }
}
