using TelecomBillingAndConsumption.Data.Helpers;

namespace TelecomBillingAndConsumption.Service.Interfaces
{
    public interface ITariffCacheService
    {
        bool TryGetPrice(UsageType type, bool isRoaming, bool isPeak, out decimal price);
        void Reload();
    }
}
