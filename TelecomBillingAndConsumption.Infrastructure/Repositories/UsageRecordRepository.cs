using TelecomBillingAndConsumption.Data.Entities;
using TelecomBillingAndConsumption.Infrastructure.DatabaseConntection;
using TelecomBillingAndConsumption.Infrastructure.InfrastructureBases;
using TelecomBillingAndConsumption.Infrastructure.Interfaces;

namespace TelecomBillingAndConsumption.Infrastructure.Repositories
{
    public class UsageRecordRepository : GenericRepository<UsageRecord>, IUsageRecordRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UsageRecordRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<UsageRecord> QueryWithIncludes()
        {
            return _dbContext.UsageRecords.AsQueryable();
        }
    }
}
