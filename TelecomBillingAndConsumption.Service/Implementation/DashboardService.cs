using Microsoft.EntityFrameworkCore;
using TelecomBillingAndConsumption.Data.Helpers;
using TelecomBillingAndConsumption.Infrastructure.Interfaces;
using TelecomBillingAndConsumption.Service.HelperDtos;
using TelecomBillingAndConsumption.Service.Interfaces;

namespace TelecomBillingAndConsumption.Service.Implementation
{
    public class DashboardService : IDashboardService
    {
        private readonly IBillRepository _billRepository;
        private readonly IUsageRecordRepository _usageRecordRepository;
        public DashboardService(
            IUsageRecordRepository usageRecordRepository,
            IBillRepository billRepository)
        {
            _billRepository = billRepository;
            _usageRecordRepository = usageRecordRepository;
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

        public async Task<UsageStatistics> GetUsageStatisticsAsync(int month, int year)
        {
            var periodStart = new DateTime(year, month, 1);
            var periodEnd = periodStart.AddMonths(1);

            var usageRecords = _usageRecordRepository.QueryWithIncludes()
                .Where(r => r.Timestamp >= periodStart && r.Timestamp < periodEnd);

            var totalCallMinutes = await usageRecords
                .Where(r => r.UsageType == UsageType.Call)
                .SumAsync(r => r.CallMinutes ?? 0);

            var totalDataMB = await usageRecords
                .Where(r => r.UsageType == UsageType.Data)
                .SumAsync(r => r.DataMB ?? 0m);

            var totalSMS = await usageRecords
                .Where(r => r.UsageType == UsageType.SMS)
                .SumAsync(r => r.SMSCount ?? 0);

            // If you store cost per usage, aggregate accordingly,
            // Otherwise, calculate cost using linked Tariff.
            var totalUsageCost = await usageRecords.SumAsync(r => r.TotalCost);

            return new UsageStatistics
            {
                TotalCallMinutes = totalCallMinutes,
                TotalDataMB = totalDataMB,
                TotalSMS = totalSMS,
                TotalUsageCost = totalUsageCost
            };
        }
    }
}
