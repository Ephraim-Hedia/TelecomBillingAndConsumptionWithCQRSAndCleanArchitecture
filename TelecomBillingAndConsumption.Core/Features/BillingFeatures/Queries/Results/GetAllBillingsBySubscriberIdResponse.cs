namespace TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Results
{
    public class GetAllBillingsBySubscriberIdResponse
    {
        public int BillId { get; set; }

        public string Month { get; set; }

        public decimal TotalAmount { get; set; }

        public bool IsPaid { get; set; }
    }
}
