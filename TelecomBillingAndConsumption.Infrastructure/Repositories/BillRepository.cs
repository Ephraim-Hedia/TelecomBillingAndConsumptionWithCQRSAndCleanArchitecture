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

        public async Task<Bill> GetByIdWithIncludesAsync(int billId)
            => await _context.Bills
                .Include(b => b.BillDetail)
                .FirstOrDefaultAsync(b => b.Id == billId);

    }
}
