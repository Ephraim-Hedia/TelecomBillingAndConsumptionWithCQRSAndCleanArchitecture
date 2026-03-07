using TelecomBillingAndConsumption.Data.Helpers;
using TelecomBillingAndConsumption.Infrastructure.Interfaces;
using TelecomBillingAndConsumption.Service.Interfaces;

namespace TelecomBillingAndConsumption.Service.Implementation
{
    public class TariffCacheService : ITariffCacheService
    {
        private readonly ITariffRepository _repository;

        private Dictionary<(UsageType, bool, bool), decimal> _tariffs = new();

        public TariffCacheService(ITariffRepository repository)
        {
            _repository = repository;
            Load();
        }

        private void Load()
        {
            var tariffs = _repository.GetTableNoTracking().ToList();

            _tariffs = tariffs.ToDictionary(
                x => (x.UsageType, x.IsRoaming, x.IsPeak),
                x => x.PricePerUnit
            );
        }

        public void Reload()
        {
            Load();
        }

        public bool TryGetPrice(UsageType type, bool isRoaming, bool isPeak, out decimal price)
        {
            return _tariffs.TryGetValue((type, isRoaming, isPeak), out price);
        }
    }
}
