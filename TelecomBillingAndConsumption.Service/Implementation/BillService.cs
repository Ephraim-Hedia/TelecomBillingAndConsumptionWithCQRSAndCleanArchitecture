using Microsoft.EntityFrameworkCore;
using TelecomBillingAndConsumption.Data.Entities;
using TelecomBillingAndConsumption.Data.Helpers;
using TelecomBillingAndConsumption.Infrastructure.Interfaces;
using TelecomBillingAndConsumption.Service.HelperDtos;
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
        private readonly ITariffCacheService _tariffCache;

        public BillService(
            ISubscriberService subscriberService,
            IPlanService planService,
            IUsageSummaryService usageSummaryService,
            IBillRepository billRepository,
            ITariffCacheService tariffCacheService)
        {
            _subscriberService = subscriberService;
            _planService = planService;
            _usageSummaryService = usageSummaryService;
            _billRepository = billRepository;
            _tariffCache = tariffCacheService;
        }

        public async Task<int> GenerateMonthlyBillAsync(int subscriberId, string month)
        {
            //-----------------------------------------
            // 1 Check if bill already exists
            //-----------------------------------------
            var existingBill = await _billRepository
                .GetBillsBySubscriberIdQuarable(subscriberId)
                .FirstOrDefaultAsync(b => b.Month == month);

            if (existingBill != null)
                throw new InvalidOperationException("Bill already generated for this subscriber and month.");


            //-----------------------------------------
            // 2 Calculate billing period
            //-----------------------------------------
            var (periodStart, periodEnd) = ParseBillingPeriod(month);

            //-----------------------------------------
            // 3 Get subscriber
            //-----------------------------------------
            var subscriber = await _subscriberService.GetByIdAsync(subscriberId);

            if (subscriber == null)
                throw new KeyNotFoundException($"Subscriber {subscriberId} not found.");

            //-----------------------------------------
            // 4 Get plan
            //-----------------------------------------
            var plan = await _planService.GetByIdAsync(subscriber.PlanId);

            if (plan == null)
                throw new InvalidOperationException("Subscriber plan not found.");

            //-----------------------------------------
            // 5 Get usage summary
            //-----------------------------------------
            var usage = await _usageSummaryService
                .GetUsageSummaryAsync(subscriberId, periodStart, periodEnd);
            if (usage == null)
                throw new InvalidOperationException("Usage summary not available.");

            //-----------------------------------------
            // 6 Tariff prices
            //-----------------------------------------
            var peakCallPrice = GetTariff(UsageType.Call, subscriber.IsRoaming, true);
            var offPeakCallPrice = GetTariff(UsageType.Call, subscriber.IsRoaming, false);
            var dataPrice = GetTariff(UsageType.Data, subscriber.IsRoaming, false);
            var smsPrice = GetTariff(UsageType.SMS, subscriber.IsRoaming, false);


            //-----------------------------------------
            // 7 Call usage calculation
            //-----------------------------------------
            var (callCost, extraCallCost) = CalculateCallCosts(usage, plan, peakCallPrice, offPeakCallPrice);


            //-----------------------------------------
            // 8 Data & SMS usage
            //-----------------------------------------
            (decimal dataCost, decimal smsCost, decimal extraUsageCost) = CalculateDataSmsCosts(usage, plan, dataPrice, smsPrice, extraCallCost);

            //-----------------------------------------
            // 9 Usage cost
            //-----------------------------------------
            var usageCost = callCost + dataCost + smsCost;


            //-----------------------------------------
            // 10 Roaming surcharge
            //-----------------------------------------
            var roamingSurcharge = subscriber.IsRoaming ? usageCost * 0.10m : 0;


            //-----------------------------------------
            // 11 Plan fee
            //-----------------------------------------
            var planFee = plan.MonthlyFee;


            //-----------------------------------------
            // 12 Subtotal
            //-----------------------------------------
            var subtotal = planFee + usageCost + extraUsageCost + roamingSurcharge;


            //-----------------------------------------
            // 13 Loyalty discount
            //-----------------------------------------
            var loyaltyDiscount = CalculateLoyaltyDiscount(subscriber, subtotal);


            //-----------------------------------------
            // 14 VAT
            //-----------------------------------------
            var vatAmount = CalculateVat(subtotal - loyaltyDiscount);


            //-----------------------------------------
            // 15 Final amount
            //-----------------------------------------
            var totalAmount = subtotal + vatAmount - loyaltyDiscount;

            //-----------------------------------------
            // 16 Bill detail
            //-----------------------------------------
            var billDetail = new BillDetail
            {
                PeakCalls = usage.PeakCalls,
                OffPeakCalls = usage.OffPeakCalls,
                DataMB = usage.UsedDataMB,
                Sms = usage.UsedSmsCount
            };


            //-----------------------------------------
            // 17 Create bill
            //-----------------------------------------
            var bill = CreateBill(subscriberId, month, planFee, usageCost, extraUsageCost, roamingSurcharge, loyaltyDiscount, vatAmount, totalAmount, billDetail);

            //-----------------------------------------
            // 18 Save
            //-----------------------------------------
            await _billRepository.AddAsync(bill);

            return bill.Id;
        }

        public IQueryable<Bill> GetAllBillsBySubsriberIdQuarable(int subscriberId)
        {
            return _billRepository.GetBillsBySubscriberIdQuarable(subscriberId);
        }
        public async Task<Bill> GetAllBillsBySubsriberIdAndMonthAsync(int subscriberId, string month)
        {
            return await _billRepository.GetBillsBySubscriberIdQuarable(subscriberId).FirstOrDefaultAsync(b => b.Month == month);
        }

        public async Task<Bill> GetBillAsync(int billId)
            => await _billRepository.GetByIdWithIncludesAsync(billId);


        #region Private Methods 
        private (DateTime start, DateTime end) ParseBillingPeriod(string month)
        {
            if (!DateTime.TryParse($"{month}-01", out var start))
                throw new ArgumentException("Invalid month format.");

            return (start, start.AddMonths(1));
        }
        private decimal GetTariff(UsageType type, bool roaming, bool peak)
        {
            if (!_tariffCache.TryGetPrice(type, roaming, peak, out var price))
                throw new InvalidOperationException($"Tariff not configured for {type} (Roaming={roaming}, Peak={peak})");

            return price;
        }
        private decimal CalculateVat(decimal subtotal)
        {
            return subtotal * 0.15m;
        }

        private decimal CalculateLoyaltyDiscount(Subscriber subscriber, decimal subtotal)
        {
            if ((DateTime.UtcNow - subscriber.CreatedAt).TotalDays > 365 * 2)
                return subtotal * 0.05m;

            return 0;
        }

        private (decimal callCost, decimal extraCallCost) CalculateCallCosts(SubscriberUsageSummaryResponse usage,
                                                                                            Plan plan,
                                                                                            decimal peakCallPrice,
                                                                                            decimal offPeakCallPrice)
        {
            var remainingBundle = plan.IncludedCallMinutes;

            var bundleOffPeak = Math.Min(usage.OffPeakCalls, remainingBundle);
            remainingBundle -= bundleOffPeak;

            var bundlePeak = Math.Min(usage.PeakCalls, remainingBundle);
            remainingBundle -= bundlePeak;

            var overOffPeak = usage.OffPeakCalls - bundleOffPeak;
            var overPeak = usage.PeakCalls - bundlePeak;

            var callCost =
                (bundlePeak * peakCallPrice) +
                (bundleOffPeak * offPeakCallPrice);

            var extraCallCost =
                (overPeak * peakCallPrice * 2) +
                (overOffPeak * offPeakCallPrice * 2);

            return (callCost, extraCallCost);
        }


        private (decimal dataCost, decimal smsCost, decimal extraUsageCost) CalculateDataSmsCosts(
                                                                            SubscriberUsageSummaryResponse usage,
                                                                            Plan plan,
                                                                            decimal dataPrice,
                                                                            decimal smsPrice,
                                                                            decimal extraCallCost)
        {
            var bundleData = Math.Min(usage.UsedDataMB, plan.IncludedDataMB);
            var bundleSms = Math.Min(usage.UsedSmsCount, plan.IncludedSMS);

            var dataCost = bundleData * dataPrice;
            var smsCost = bundleSms * smsPrice;

            var overData = Math.Max(usage.UsedDataMB - plan.IncludedDataMB, 0);
            var overSms = Math.Max(usage.UsedSmsCount - plan.IncludedSMS, 0);

            var extraUsageCost =
                extraCallCost +
                (overData * dataPrice * 2) +
                (overSms * smsPrice * 2);

            return (dataCost, smsCost, extraUsageCost);
        }

        private Bill CreateBill(
                                int subscriberId,
                                string month,
                                decimal planFee,
                                decimal usageCost,
                                decimal extraUsageCost,
                                decimal roamingSurcharge,
                                decimal loyaltyDiscount,
                                decimal vatAmount,
                                decimal totalAmount,
                                BillDetail billDetail)
        {
            return new Bill
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
        }
        #endregion
    }
}
