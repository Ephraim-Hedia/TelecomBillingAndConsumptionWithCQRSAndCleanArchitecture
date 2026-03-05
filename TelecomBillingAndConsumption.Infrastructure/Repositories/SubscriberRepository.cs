using Microsoft.EntityFrameworkCore;
using TelecomBillingAndConsumption.Data.Entities;
using TelecomBillingAndConsumption.Infrastructure.DatabaseConntection;
using TelecomBillingAndConsumption.Infrastructure.InfrastructureBases;
using TelecomBillingAndConsumption.Infrastructure.Interfaces;

namespace TelecomBillingAndConsumption.Infrastructure.Repositories
{
    public class SubscriberRepository : GenericRepository<Subscriber>, ISubscriberRepository
    {
        public SubscriberRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Subscriber> GetSubscriberByIdWithIncludes(int id)
        {
            return await GetTableNoTracking()
                .Include(s => s.Plan)
                .Include(s => s.Bills)
                .Include(s => s.UsageRecords).FirstOrDefaultAsync(s => s.Id == id);  // Add more Includes if needed
        }

        public IQueryable<Subscriber> QueryWithIncludes()
        {
            return GetTableNoTracking()
            .Include(s => s.Plan)
            .Include(s => s.Bills)
            .Include(s => s.UsageRecords)
            .Where(s => !s.IsDeleted);  // Add more Includes if needed
        }
        public async Task<bool> UpdateSubscriberPlanAsync(int subscriberId, int newPlanId)
        {
            var subscriber = await GetByIdAsync(subscriberId);
            if (subscriber == null || subscriber.IsDeleted)
                return false;

            subscriber.PlanId = newPlanId;
            await UpdateAsync(subscriber);
            return true;
        }
    }
}
