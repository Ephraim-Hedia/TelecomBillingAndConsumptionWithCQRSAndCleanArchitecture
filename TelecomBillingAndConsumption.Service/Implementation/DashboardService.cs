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
            var topCustomers = await _billRepository
                        .GetTableNoTracking()
                        .GroupBy(b => b.SubscriberId)
                        .Select(g => new TopCustomerDto
                        {
                            SubscriberId = g.Key,
                            TotalCost = g.Sum(b => b.TotalAmount ?? 0),
                            Name = g.Select(b => b.Subscriber.Name).FirstOrDefault(),
                            PhoneNumber = g.Select(b => b.Subscriber.PhoneNumber).FirstOrDefault()
                        })
                        .OrderByDescending(x => x.TotalCost)
                        .Take(topN)
                        .ToListAsync();

            return topCustomers;
        }


        public async Task<GetDashboardRevenue> GetDashboardRevenueAsync(int month, int year)
        {
            var result = await _billRepository
                                                .GetTableNoTracking()
                                                .Where(b => b.Month == $"{year:D4}-{month:D2}")
                                                .GroupBy(x => 1)
                                                .Select(g => new
                                                {
                                                    Total = g.Sum(x => x.TotalAmount ?? 0),
                                                    Paid = g.Where(x => x.IsPaid == true).Sum(x => x.TotalAmount ?? 0),
                                                    Unpaid = g.Where(x => x.IsPaid == false).Sum(x => x.TotalAmount ?? 0)
                                                })
                                                .FirstOrDefaultAsync();

            return new GetDashboardRevenue
            {
                Month = month,
                Year = year,
                TotalRevenue = result?.Total ?? 0,
                PaidBills = result?.Paid ?? 0,
                UnpaidBills = result?.Unpaid ?? 0
            };
        }

        public async Task<UsageStatistics> GetUsageStatisticsAsync(int month, int year)
        {
            var periodStart = new DateTime(year, month, 1);
            var periodEnd = periodStart.AddMonths(1);
            var stats = await _usageRecordRepository
                                                    .GetTableNoTracking()
                                                    .Where(r => r.Timestamp >= periodStart && r.Timestamp < periodEnd)
                                                    .GroupBy(x => 1)
                                                    .Select(g => new
                                                    {
                                                        Calls = g.Where(x => x.UsageType == UsageType.Call)
                                                                    .Sum(x => x.CallMinutes ?? 0),

                                                        Data = g.Where(x => x.UsageType == UsageType.Data)
                                                                .Sum(x => x.DataMB ?? 0),

                                                        Sms = g.Where(x => x.UsageType == UsageType.SMS)
                                                                .Sum(x => x.SMSCount ?? 0),

                                                        Cost = g.Sum(x => x.TotalCost)
                                                    })
                                                    .FirstOrDefaultAsync();

            // If you store cost per usage, aggregate accordingly,
            // Otherwise, calculate cost using linked Tariff.


            return new UsageStatistics
            {
                TotalCallMinutes = stats?.Calls ?? 0,
                TotalDataMB = stats?.Data ?? 0,
                TotalSMS = stats?.Sms ?? 0,
                TotalUsageCost = stats?.Cost ?? 0
            };
        }
    }
}
