
using Microsoft.EntityFrameworkCore;
using TelecomBillingAndConsumption.Data.Entities;
using TelecomBillingAndConsumption.Data.Helpers;
using TelecomBillingAndConsumption.Infrastructure.InfrastructureBases;
using TelecomBillingAndConsumption.Service.Interfaces;

namespace TelecomBillingAndConsumption.Service.Implementation
{
    public class TariffService : ITariffService
    {
        #region Fields
        private readonly IGenericRepository<TariffRule> _tariffRepository;
        private readonly ITariffCacheService _tariffCacheService;
        #endregion

        #region Constructors
        public TariffService(
            IGenericRepository<TariffRule> tariffRepository
            , ITariffCacheService tariffCacheService)
        {
            _tariffRepository = tariffRepository;
            _tariffCacheService = tariffCacheService;
        }
        #endregion

        #region Handle Functions

        public IQueryable<TariffRule> QueryTariffs()
        {
            return _tariffRepository
                .GetTableNoTracking()
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.UsageType)
                .ThenBy(x => x.IsRoaming)
                .ThenBy(x => x.IsPeak);
        }

        public async Task<List<TariffRule>> GetAllAsync()
        {
            return await QueryTariffs().ToListAsync();
        }

        public async Task<TariffRule?> GetByIdAsync(int id)
        {
            var rule = await _tariffRepository.GetByIdAsync(id);
            return (rule != null && !rule.IsDeleted) ? rule : null;
        }

        public async Task<int> AddAsync(TariffRule rule)
        {
            var result = await _tariffRepository.AddAsync(rule);
            _tariffCacheService.Reload();
            return result.Id;
        }

        public async Task<bool> UpdateAsync(TariffRule rule)
        {
            await _tariffRepository.UpdateAsync(rule);
            _tariffCacheService.Reload();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var rule = await _tariffRepository.GetByIdAsync(id);
            if (rule == null || rule.IsDeleted)
                return false;
            rule.IsDeleted = true;
            await _tariffRepository.UpdateAsync(rule);
            _tariffCacheService.Reload();
            return true;
        }

        public TariffRule? FindTariff(UsageType usageType, bool isRoaming, bool isPeak)
        {
            if (!_tariffCacheService.TryGetPrice(usageType, isRoaming, isPeak, out var price))
                return null;

            return new TariffRule
            {
                UsageType = usageType,
                IsRoaming = isRoaming,
                IsPeak = isPeak,
                PricePerUnit = price
            };
        }
        public decimal GetPrice(UsageType usageType, bool isRoaming, bool isPeak)
        {
            if (!_tariffCacheService.TryGetPrice(usageType, isRoaming, isPeak, out var price))
                throw new Exception($"Tariff not found for {usageType} Roaming:{isRoaming} Peak:{isPeak}");

            return price;
        }
        #endregion




    }
}
