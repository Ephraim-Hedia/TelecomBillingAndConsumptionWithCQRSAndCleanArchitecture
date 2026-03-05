using TelecomBillingAndConsumption.Data.Entities;
using TelecomBillingAndConsumption.Infrastructure.Interfaces;
using TelecomBillingAndConsumption.Service.Interfaces;
using TelecomBillingAndConsumption.Service.Interfaces.PlanService;

namespace TelecomBillingAndConsumption.Service.Implementation
{
    public class BillService : IBillService
    {
        private readonly ISubscriberService _subscriberService;
        private readonly IPlanService _planService;
        private readonly IUsageSummaryService _usageSummaryService;
        private readonly IBillRepository _billRepository;

        public BillService(
            ISubscriberService subscriberService,
            IPlanService planService,
            IUsageSummaryService usageSummaryService,
            IBillRepository billRepository)
        {
            _subscriberService = subscriberService;
            _planService = planService;
            _usageSummaryService = usageSummaryService;
            _billRepository = billRepository;
        }

        public async Task<int> GenerateMonthlyBillAsync(int subscriberId, string month)
        {
            // 1. Compute billing period (e.g., month "2026-03")
            var periodStart = DateTime.Parse($"{month}-01");
            var periodEnd = periodStart.AddMonths(1);

            // 2. Fetch subscriber & plan
            var subscriber = await _subscriberService.GetByIdAsync(subscriberId);
            if (subscriber == null)
                throw new KeyNotFoundException("Subscriber not found.");

            var plan = await _planService.GetByIdAsync(subscriber.PlanId);
            if (plan == null)
                throw new InvalidOperationException("Subscriber plan not found.");

            // 3. Use UsageSummaryService to aggregate all usage
            var usageSummary = await _usageSummaryService.GetUsageSummaryAsync(subscriberId, periodStart, periodEnd);

            // 4. Calculate plan fee
            var planFee = plan.MonthlyFee;

            // 5. Calculate usage cost (within bundle)
            var usageCost =
                (Math.Min(usageSummary.UsedCallMinutes, usageSummary.CallMinutesBundle) * usageSummary.CurrentCallUnitPrice) +
                (Math.Min(usageSummary.UsedDataMB, usageSummary.DataBundleMB) * usageSummary.CurrentDataUnitPrice) +
                (Math.Min(usageSummary.UsedSmsCount, usageSummary.SmsBundle) * usageSummary.CurrentSmsUnitPrice);

            // 6. Calculate extra usage cost (over bundle)
            var extraUsageCost =
                (usageSummary.CallMinutesOverBundle * usageSummary.OverageCallUnitPrice) +
                (usageSummary.DataMBOverBundle * usageSummary.OverageDataUnitPrice) +
                (usageSummary.SmsOverBundle * usageSummary.OverageSmsUnitPrice);

            // 7. Calculate roaming surcharge if needed (example: all roaming call/data/sms cost at roaming rate)
            var roamingSurcharge = 0m; // you can add custom logic to extract roaming usage from summary + apply tariff

            // 8. Calculate loyalty discount (simple example: -5% if subscriber more than 12 months)
            var loyaltyDiscount = 0m;
            if ((DateTime.UtcNow - subscriber.CreatedAt).TotalDays > 365 * 2)
                loyaltyDiscount = (usageCost + extraUsageCost) * 0.05m; // -5%

            // 9. Calculate VAT
            var vatPercent = 0.15m; // 15% VAT example
            var vatAmount = (planFee + usageCost + extraUsageCost + roamingSurcharge - loyaltyDiscount) * vatPercent;

            // 10. Calculate total amount
            var totalAmount = planFee + usageCost + extraUsageCost + roamingSurcharge + vatAmount - loyaltyDiscount;

            // 11. Create BillDetail
            var billDetail = new BillDetail
            {
                PeakCalls = usageSummary.PeakCalls,
                OffPeakCalls = usageSummary.OffPeakCalls,
                DataMB = usageSummary.UsedDataMB,
                Sms = usageSummary.UsedSmsCount,

                // You can add UsageRecord FK if needed for audit/itemized
            };

            // 12. Create Bill entity
            var bill = new Bill
            {
                SubscriberId = subscriberId,
                Month = month,
                PlanFee = planFee,
                UsageCost = usageCost,
                ExtraUsageCost = extraUsageCost,
                RoamingSurcharge = roamingSurcharge,
                LoyaltyDiscount = loyaltyDiscount,
                VatAmount = vatAmount,
                TotalAmount = totalAmount,
                IsPaid = false,
                BillDetail = billDetail
            };

            // 13. Save to the database
            await _billRepository.AddAsync(bill);

            // 14. Return bill
            return bill.Id;
        }

        public async Task<Bill> GetBillAsync(int billId)
            => await _billRepository.GetByIdWithIncludesAsync(billId);
    }
}
