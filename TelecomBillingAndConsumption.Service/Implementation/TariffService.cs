
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
        #endregion

        #region Constructors
        public TariffService(IGenericRepository<TariffRule> tariffRepository)
        {
            _tariffRepository = tariffRepository;
        }
        #endregion

        #region Handle Functions

        public IQueryable<TariffRule> QueryTariffs()
        {
            return _tariffRepository.GetTableNoTracking().Where(x => !x.IsDeleted);
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
            return result.Id;
        }

        public async Task<bool> UpdateAsync(TariffRule rule)
        {
            await _tariffRepository.UpdateAsync(rule);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var rule = await _tariffRepository.GetByIdAsync(id);
            if (rule == null || rule.IsDeleted)
                return false;
            rule.IsDeleted = true;
            await _tariffRepository.UpdateAsync(rule);
            return true;
        }

        public async Task<TariffRule?> FindTariffAsync(UsageType usageType, bool isRoaming, bool isPeak)
        {
            return await _tariffRepository.GetTableNoTracking()
                .FirstOrDefaultAsync(t =>
                    t.UsageType == usageType &&
                    t.IsRoaming == isRoaming &&
                    t.IsPeak == isPeak &&
                    !t.IsDeleted);
        }
        #endregion
    }
}
