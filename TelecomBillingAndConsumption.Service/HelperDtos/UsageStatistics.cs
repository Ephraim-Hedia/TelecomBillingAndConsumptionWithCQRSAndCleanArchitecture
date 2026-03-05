namespace TelecomBillingAndConsumption.Service.HelperDtos
{
    public class UsageStatistics
    {
        public int TotalCallMinutes { get; set; }

        public decimal TotalDataMB { get; set; }

        public int TotalSMS { get; set; }

        public decimal TotalUsageCost { get; set; }
    }
}
