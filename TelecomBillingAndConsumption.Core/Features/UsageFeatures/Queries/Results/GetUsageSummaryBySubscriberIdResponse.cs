namespace TelecomBillingAndConsumption.Core.Features.UsageFeatures.Queries.Results
{
    public class GetUsageSummaryBySubscriberIdResponse
    {
        public int TotalCallMinutes { get; set; }

        public decimal TotalDataMB { get; set; }

        public int TotalSMS { get; set; }

        public decimal EstimatedCost { get; set; }
    }
}
