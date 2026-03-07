using Microsoft.EntityFrameworkCore;
using TelecomBillingAndConsumption.Data.Helpers;
using TelecomBillingAndConsumption.Infrastructure.Interfaces;
using TelecomBillingAndConsumption.Service.HelperDtos;
using TelecomBillingAndConsumption.Service.Interfaces;
using TelecomBillingAndConsumption.Service.Interfaces.PlanService;

public class UsageSummaryService : IUsageSummaryService
{
    private readonly ISubscriberService _subscriberService;
    private readonly IPlanService _planService;
    private readonly IUsageRecordRepository _usageRecordRepository;
    private readonly ITariffCacheService _tariffCacheService;

    public UsageSummaryService(
        ITariffCacheService tariffCacheService,
        IUsageRecordRepository usageRecordRepository,
        ISubscriberService subscriberService,
        IPlanService planService,
        IUsageRecordService usageRecordService,
        ITariffService tariffService)
    {
        _tariffCacheService = tariffCacheService;
        _usageRecordRepository = usageRecordRepository;
        _subscriberService = subscriberService;
        _planService = planService;
    }

    public async Task<SubscriberUsageSummaryResponse> GetUsageSummaryAsync(int subscriberId, DateTime periodStart, DateTime periodEnd)
    {
        if (periodEnd <= periodStart)
            throw new ArgumentException("Invalid period range.");
        //-----------------------------------------
        // 1. Get subscriber
        //-----------------------------------------
        var subscriber = await _subscriberService.GetByIdAsync(subscriberId);
        if (subscriber == null)
            return null;

        //-----------------------------------------
        // 2. Get plan
        //-----------------------------------------
        var plan = await _planService.GetByIdAsync(subscriber.PlanId);
        if (plan == null)
            return null;

        //-----------------------------------------
        // 3. Aggregate usage in DB
        //-----------------------------------------
        var usage = await _usageRecordRepository
            .GetTableNoTracking()
            .Where(r =>
                r.SubscriberId == subscriberId &&
                r.Timestamp >= periodStart &&
                r.Timestamp < periodEnd &&
                !r.IsDeleted)
            .GroupBy(x => 1)
            .Select(g => new
            {
                UsedCallMinutes = g.Sum(x => x.CallMinutes ?? 0),
                UsedDataMB = g.Sum(x => x.DataMB ?? 0),
                UsedSms = g.Sum(x => x.SMSCount ?? 0),

                PeakCalls = g.Where(x => x.UsageType == UsageType.Call && x.IsPeak)
                    .Sum(x => x.CallMinutes ?? 0),

                OffPeakCalls = g.Where(x => x.UsageType == UsageType.Call && !x.IsPeak)
                    .Sum(x => x.CallMinutes ?? 0)
            })
            .FirstOrDefaultAsync();

        //-----------------------------------------
        // 4. If no usage found
        //-----------------------------------------
        var usedCallMinutes = usage?.UsedCallMinutes ?? 0;
        var usedDataMB = usage?.UsedDataMB ?? 0;
        var usedSms = usage?.UsedSms ?? 0;
        var peakCalls = usage?.PeakCalls ?? 0;
        var offPeakCalls = usage?.OffPeakCalls ?? 0;

        //-----------------------------------------
        // 5. Bundle calculations
        //-----------------------------------------
        var (callLeft, callOver) = CalculateBundle(plan.IncludedCallMinutes, usedCallMinutes);
        var (dataLeft, dataOver) = CalculateBundle(plan.IncludedDataMB, usedDataMB);
        var (smsLeft, smsOver) = CalculateBundle(plan.IncludedSMS, usedSms);


        //-----------------------------------------
        // 6. Tariff lookup (from cache)
        //-----------------------------------------
        var peakCallPrice = GetTariff(UsageType.Call, subscriber.IsRoaming, true);
        var offPeakCallPrice = GetTariff(UsageType.Call, subscriber.IsRoaming, false);
        var dataPrice = GetTariff(UsageType.Data, subscriber.IsRoaming, false);
        var smsPrice = GetTariff(UsageType.SMS, subscriber.IsRoaming, false);

        //-----------------------------------------
        // 7. Overage status
        //-----------------------------------------
        bool isCallOverage = callLeft == 0;
        bool isDataOverage = dataLeft == 0;
        bool isSmsOverage = smsLeft == 0;

        //-----------------------------------------
        // 8. Create DTO
        //-----------------------------------------
        return new SubscriberUsageSummaryResponse
        {
            SubscriberId = subscriber.Id,
            SubscriberPhone = subscriber.PhoneNumber,
            PlanName = plan.Name,

            UsedCallMinutes = usedCallMinutes,
            UsedDataMB = usedDataMB,
            UsedSmsCount = usedSms,

            CallMinutesBundle = plan.IncludedCallMinutes,
            DataBundleMB = plan.IncludedDataMB,
            SmsBundle = plan.IncludedSMS,

            CallMinutesLeft = callLeft,
            DataMBLeft = dataLeft,
            SmsLeft = smsLeft,

            CallMinutesOverBundle = callOver,
            DataMBOverBundle = dataOver,
            SmsOverBundle = smsOver,

            PeakCalls = peakCalls,
            OffPeakCalls = offPeakCalls,

            CurrentCallUnitPrice = isCallOverage ? offPeakCallPrice * 2 : offPeakCallPrice,
            CurrentDataUnitPrice = isDataOverage ? dataPrice * 2 : dataPrice,
            CurrentSmsUnitPrice = isSmsOverage ? smsPrice * 2 : smsPrice,

            OverageCallUnitPrice = offPeakCallPrice * 2,
            OverageDataUnitPrice = dataPrice * 2,
            OverageSmsUnitPrice = smsPrice * 2,

            IsCallOverage = isCallOverage,
            IsDataOverage = isDataOverage,
            IsSmsOverage = isSmsOverage,

            PeriodStart = periodStart,
            PeriodEnd = periodEnd
        };
    }

    private decimal GetTariff(UsageType type, bool roaming, bool peak)
    {
        if (!_tariffCacheService.TryGetPrice(type, roaming, peak, out var price))
            throw new InvalidOperationException($"Tariff not configured for {type} (Roaming={roaming}, Peak={peak})");

        return price;
    }
    private (int left, int over) CalculateBundle(int bundle, int used)
    {
        return (
            Math.Max(0, bundle - used),
            Math.Max(0, used - bundle)
        );
    }
    private (decimal left, decimal over) CalculateBundle(decimal bundle, decimal used)
    {
        return (
            Math.Max(0, bundle - used),
            Math.Max(0, used - bundle)
        );
    }
}