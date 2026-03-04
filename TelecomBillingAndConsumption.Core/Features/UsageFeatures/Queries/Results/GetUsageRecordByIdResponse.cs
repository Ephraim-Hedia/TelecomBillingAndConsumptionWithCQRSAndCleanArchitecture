using TelecomBillingAndConsumption.Data.Helpers;

namespace TelecomBillingAndConsumption.Core.Features.UsageFeatures.Queries.Results
{
    public class GetUsageRecordByIdResponse
    {
        public int Id { get; set; }

        public int SubscriberId { get; set; }

        public string SubscriberPhone { get; set; }

        public UsageType UsageType { get; set; }

        public int? CallMinutes { get; set; }

        public decimal? DataMB { get; set; }

        public int? SMSCount { get; set; }

        public bool IsRoaming { get; set; }

        public bool IsPeak { get; set; }

        public DateTime Timestamp { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalCost { get; set; }
    }
}
