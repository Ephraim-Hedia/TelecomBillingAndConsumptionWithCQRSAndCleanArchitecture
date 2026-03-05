using TelecomBillingAndConsumption.Service.HelperDtos;

namespace TelecomBillingAndConsumption.Service.Interfaces
{
    public interface IDashboardService
    {
        Task<List<TopCustomerDto>> GetTopCustomersAsync(int topN);

        Task<GetDashboardRevenue> GetDashboardRevenueAsync(int month, int year);

        public Task<UsageStatistics> GetUsageStatisticsAsync(int month, int year);
    }
}
