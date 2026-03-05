using TelecomBillingAndConsumption.Service.HelperDtos;

namespace TelecomBillingAndConsumption.Service.Interfaces
{
    public interface IDashboardService
    {
        Task<List<TopCustomerDto>> GetTopCustomersAsync(int topN);
    }
}
