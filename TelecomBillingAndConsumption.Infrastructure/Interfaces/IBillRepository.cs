using TelecomBillingAndConsumption.Data.Entities;
using TelecomBillingAndConsumption.Infrastructure.InfrastructureBases;

namespace TelecomBillingAndConsumption.Infrastructure.Interfaces
{
    public interface IBillRepository : IGenericRepository<Bill>
    {

        public Task<Bill> GetByIdWithIncludesAsync(int billId);
        public IQueryable<Bill> QueryWithIncludes();
        public IQueryable<Bill> GetBillsBySubscriberIdQuarable(int subscriberId);

    }
}
