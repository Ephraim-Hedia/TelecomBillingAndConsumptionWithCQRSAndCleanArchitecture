namespace TelecomBillingAndConsumption.Data.Entities
{
    public class BillDetail : BaseEntity
    {
        public int BillId { get; set; } // Foreign Key
        public int PeakCalls { get; set; }
        public int OffPeakCalls { get; set; }
        public decimal DataMB { get; set; }
        public int Sms { get; set; }

        // Navigation
        public Bill Bill { get; set; } = null!;

        public UsageRecord UsageRecord { get; set; } = null!;
    }
}
