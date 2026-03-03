namespace TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Results
{
    public class GetAllBillingsBySubscriberIdResponse
    {
        public int BillId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public decimal TotalAmount { get; set; }

        public bool IsPaid { get; set; }
    }
}
