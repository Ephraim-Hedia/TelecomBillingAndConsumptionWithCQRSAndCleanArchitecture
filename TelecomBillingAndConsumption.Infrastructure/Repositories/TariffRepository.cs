
using TelecomBillingAndConsumption.Data.Entities;
using TelecomBillingAndConsumption.Infrastructure.DatabaseConntection;
using TelecomBillingAndConsumption.Infrastructure.InfrastructureBases;
using TelecomBillingAndConsumption.Infrastructure.Interfaces;

namespace TelecomBillingAndConsumption.Infrastructure.Repositories
{
    public class TariffRepository : GenericRepository<TariffRule>, ITariffRepository
    {
        public TariffRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
