using Microsoft.EntityFrameworkCore;
using TelecomBillingAndConsumption.Data.Entities;
using TelecomBillingAndConsumption.Infrastructure.DatabaseConntection;
using TelecomBillingAndConsumption.Infrastructure.InfrastructureBases;
using TelecomBillingAndConsumption.Infrastructure.Interfaces;

namespace TelecomBillingAndConsumption.Infrastructure.Repositories
{
    public class BillRepository : GenericRepository<Bill>, IBillRepository
    {
        private readonly ApplicationDbContext _context;
        public BillRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public IQueryable<Bill> GetBillsBySubscriberIdQuarable(int subscriberId)
            => _context.Bills
                .Where(b => b.SubscriberId == subscriberId && !b.IsDeleted);


        public async Task<Bill> GetByIdWithIncludesAsync(int billId)
            => await _context.Bills
                .Include(b => b.BillDetail)
                .FirstOrDefaultAsync(b => b.Id == billId);


        public IQueryable<Bill> QueryWithIncludes()
        {
            return GetTableNoTracking()
            .Include(s => s.BillDetail)
            .Include(s => s.Subscriber)
            .Where(s => !s.IsDeleted);  // Add more Includes if needed
        }

    }
}
