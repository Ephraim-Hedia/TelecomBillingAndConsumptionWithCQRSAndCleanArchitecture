using TelecomBillingAndConsumption.Data.Entities;
using TelecomBillingAndConsumption.Infrastructure.InfrastructureBases;

namespace TelecomBillingAndConsumption.Infrastructure.Interfaces
{
    public interface ISubscriberRepository : IGenericRepository<Subscriber>
    {
        IQueryable<Subscriber> QueryWithIncludes();
        Task<Subscriber> GetSubscriberByIdWithIncludes(int id);
        public Task<bool> UpdateSubscriberPlanAsync(int subscriberId, int newPlanId);
    }
}
