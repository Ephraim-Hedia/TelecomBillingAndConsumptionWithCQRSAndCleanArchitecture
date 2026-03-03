namespace TelecomBillingAndConsumption.Core.Features.DashboardFeatures.Queries.Results
{
    public class UsageStatisticsResponse
    {
        public int TotalCallMinutes { get; set; }

        public decimal TotalDataMB { get; set; }

        public int TotalSMS { get; set; }

        public decimal TotalUsageCost { get; set; }
    }
}
