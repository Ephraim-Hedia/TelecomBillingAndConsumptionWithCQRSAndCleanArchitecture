using Microsoft.EntityFrameworkCore;
using TelecomBillingAndConsumption.Infrastructure.Interfaces;
using TelecomBillingAndConsumption.Service.HelperDtos;
using TelecomBillingAndConsumption.Service.Interfaces;

namespace TelecomBillingAndConsumption.Service.Implementation
{
    public class DashboardService : IDashboardService
    {
        private readonly IBillRepository _billRepository;
        public DashboardService(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }
        public async Task<List<TopCustomerDto>> GetTopCustomersAsync(int topN)
        {
            // Query the bills, include subscriber for extra info
            var query = _billRepository.QueryWithIncludes();

            var topCustomers = await query
                .GroupBy(b => b.SubscriberId)
                .Select(g => new
                {
                    SubscriberId = g.Key,
                    TotalCost = g.Sum(b => b.TotalAmount),
                    Name = g.Select(b => b.Subscriber.Name).FirstOrDefault(),
                    PhoneNumber = g.Select(b => b.Subscriber.PhoneNumber).FirstOrDefault()
                })
                .OrderByDescending(x => x.TotalCost)
                .Take(topN)
                .ToListAsync();

            // Map to DTO (here or with AutoMapper)
            return topCustomers.Select(x => new TopCustomerDto
            {
                SubscriberId = x.SubscriberId,
                TotalCost = x.TotalCost.Value,
                Name = x.Name,
                PhoneNumber = x.PhoneNumber
            }).ToList();
        }
    }
}
