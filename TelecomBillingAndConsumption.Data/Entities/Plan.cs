namespace TelecomBillingAndConsumption.Data.Entities
{
    public class Plan : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public decimal MonthlyFee { get; set; }

        public int IncludedCallMinutes { get; set; }

        public decimal IncludedDataMB { get; set; }

        public int IncludedSMS { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<Subscriber> Subscribers { get; set; } = new HashSet<Subscriber>();
    }
}
