using TelecomBillingAndConsumption.Data.Entities;
using TelecomBillingAndConsumption.Data.Helpers;

namespace TelecomBillingAndConsumption.Service.Interfaces
{
    public interface ITariffService
    {
        IQueryable<TariffRule> QueryTariffs();
        Task<List<TariffRule>> GetAllAsync();
        Task<int> AddAsync(TariffRule tariffRule);
        Task<bool> UpdateAsync(TariffRule tariffRule);
        Task<bool> DeleteAsync(int id);
        Task<TariffRule?> GetByIdAsync(int id);
        Task<TariffRule?> FindTariffAsync(UsageType usageType, bool isRoaming, bool isPeak);
    }
}
