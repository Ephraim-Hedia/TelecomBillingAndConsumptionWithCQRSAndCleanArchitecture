using TelecomBillingAndConsumption.Data.Entities;
using TelecomBillingAndConsumption.Infrastructure.DatabaseConntection;
using TelecomBillingAndConsumption.Infrastructure.InfrastructureBases;
using TelecomBillingAndConsumption.Infrastructure.Interfaces;

namespace TelecomBillingAndConsumption.Infrastructure.Repositories
{
    public class PlanRepository : GenericRepository<Plan>, IPlanRepository
    {
        public PlanRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
