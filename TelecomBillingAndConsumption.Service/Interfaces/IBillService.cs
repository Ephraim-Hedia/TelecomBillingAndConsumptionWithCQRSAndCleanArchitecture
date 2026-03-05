using TelecomBillingAndConsumption.Data.Entities;

namespace TelecomBillingAndConsumption.Service.Interfaces
{
    public interface IBillService
    {
        Task<int> GenerateMonthlyBillAsync(int subscriberId, string month);
        Task<Bill> GetBillAsync(int billId);
    }
}
