namespace TelecomBillingAndConsumption.Core.Features.DashboardFeatures.Queries.Results
{
    public class GetDashboardTopCustomersPaginatedResponse
    {
        public int SubscriberId { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public decimal TotalBillingAmount { get; set; }
    }
}
