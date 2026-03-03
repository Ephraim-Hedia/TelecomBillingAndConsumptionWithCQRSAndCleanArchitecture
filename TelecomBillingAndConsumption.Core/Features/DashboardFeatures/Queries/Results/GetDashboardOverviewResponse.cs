namespace TelecomBillingAndConsumption.Core.Features.DashboardFeatures.Queries.Results
{
    public class GetDashboardOverviewResponse
    {

        public int TotalSubscribers { get; set; }

        public int ActiveSubscribers { get; set; }

        public int TotalBillsGenerated { get; set; }

        public decimal TotalRevenue { get; set; }

        public decimal TotalOutstandingPayments { get; set; }
    }
}
