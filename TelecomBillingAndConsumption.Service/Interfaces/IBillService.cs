using TelecomBillingAndConsumption.Data.Entities;

namespace TelecomBillingAndConsumption.Service.Interfaces
{
    public interface IBillService
    {
        public Task<int> GenerateMonthlyBillAsync(int subscriberId, string month);
        public Task<Bill> GetBillAsync(int billId);
        public IQueryable<Bill> GetAllBillsBySubsriberIdQuarable(int subscriberId);
        public Task<Bill> GetAllBillsBySubsriberIdAndMonthAsync(int subscriberId, string month);

    }
}
