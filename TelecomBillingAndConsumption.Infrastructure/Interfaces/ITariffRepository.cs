using TelecomBillingAndConsumption.Data.Entities;
using TelecomBillingAndConsumption.Infrastructure.InfrastructureBases;

namespace TelecomBillingAndConsumption.Infrastructure.Interfaces
{
    public interface ITariffRepository : IGenericRepository<TariffRule>
    {
    }
}
