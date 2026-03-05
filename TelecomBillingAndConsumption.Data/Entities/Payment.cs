namespace TelecomBillingAndConsumption.Data.Entities
{
    public class Payment : BaseEntity
    {
        //public int BillId { get; set; }

        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;

        public string TransactionId { get; set; } = string.Empty;

        public DateTime PaymentDate { get; set; }

        public string Status { get; set; } = string.Empty;

        // Navigation
        //public Bill Bill { get; set; } = null!;
    }
}
