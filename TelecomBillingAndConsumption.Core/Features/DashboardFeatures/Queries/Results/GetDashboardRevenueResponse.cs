namespace TelecomBillingAndConsumption.Core.Features.DashboardFeatures.Queries.Results
{
    public class GetDashboardRevenueResponse
    {
        public int Month { get; set; }

        public int Year { get; set; }

        public decimal TotalRevenue { get; set; }

        public decimal PaidBills { get; set; }

        public decimal UnpaidBills { get; set; }
    }
}
