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


        public async Task<GetDashboardRevenue> GetDashboardRevenueAsync(int month, int year)
        {
            var bills = _billRepository.QueryWithIncludes()
                .Where(b => b.Month == $"{year:D4}-{month:D2}"); // Format "YYYY-MM"

            var totalRevenue = await bills.SumAsync(b => b.TotalAmount ?? 0);
            var paidBills = await bills.Where(b => b.IsPaid == true).SumAsync(b => b.TotalAmount ?? 0);
            var unpaidBills = await bills.Where(b => b.IsPaid == false).SumAsync(b => b.TotalAmount ?? 0);

            return new GetDashboardRevenue
            {
                Month = month,
                Year = year,
                TotalRevenue = totalRevenue,
                PaidBills = paidBills,
                UnpaidBills = unpaidBills
            };
        }
    }
}
