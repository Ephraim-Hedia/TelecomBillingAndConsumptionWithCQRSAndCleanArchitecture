using TelecomBillingAndConsumption.Data.Helpers;
using TelecomBillingAndConsumption.Service.HelperDtos;
using TelecomBillingAndConsumption.Service.Interfaces;
using TelecomBillingAndConsumption.Service.Interfaces.PlanService;

public class UsageSummaryService : IUsageSummaryService
{
    private readonly ISubscriberService _subscriberService;
    private readonly IPlanService _planService;
    private readonly IUsageRecordService _usageRecordService;
    private readonly ITariffService _tariffService;

    public UsageSummaryService(
        ISubscriberService subscriberService,
        IPlanService planService,
        IUsageRecordService usageRecordService,
        ITariffService tariffService)
    {
        _subscriberService = subscriberService;
        _planService = planService;
        _usageRecordService = usageRecordService;
        _tariffService = tariffService;
    }

    public async Task<SubscriberUsageSummaryResponse> GetUsageSummaryAsync(int subscriberId, DateTime periodStart, DateTime periodEnd)
    {
        // 1. Get subscriber & plan
        var subscriber = await _subscriberService.GetByIdAsync(subscriberId);
        if (subscriber == null)
            return null;

        var plan = await _planService.GetByIdAsync(subscriber.PlanId);
        if (plan == null)
            return null;

        // 2. Get usage records for period
        var usageRecords = await _usageRecordService.GetBySubscriberAsync(subscriber.Id);
        var usageInPeriod = usageRecords
            .Where(r => r.Timestamp >= periodStart && r.Timestamp < periodEnd)
            .ToList();

        // 3. Aggregate usage totals
        int usedCallMinutes = usageInPeriod.Sum(r => r.CallMinutes ?? 0);
        decimal usedDataMB = usageInPeriod.Sum(r => r.DataMB ?? 0m);
        int usedSmsCount = usageInPeriod.Sum(r => r.SMSCount ?? 0);

        int peakCalls = usageInPeriod
            .Where(r => r.UsageType == UsageType.Call && r.IsPeak)
            .Sum(r => r.CallMinutes ?? 0);
        int offPeakCalls = usageInPeriod
            .Where(r => r.UsageType == UsageType.Call && !r.IsPeak)
            .Sum(r => r.CallMinutes ?? 0);

        // 4. Calculate left/over-bundle values
        int callLeft = Math.Max(0, plan.IncludedCallMinutes - usedCallMinutes);
        int callOverBundle = Math.Max(0, usedCallMinutes - plan.IncludedCallMinutes);

        decimal dataLeft = Math.Max(0, plan.IncludedDataMB - usedDataMB);
        decimal dataOverBundle = Math.Max(0, usedDataMB - plan.IncludedDataMB);

        int smsLeft = Math.Max(0, plan.IncludedSMS - usedSmsCount);
        int smsOverBundle = Math.Max(0, usedSmsCount - plan.IncludedSMS);

        // 5. Tariff lookup
        var callTariff = await _tariffService.FindTariffAsync(UsageType.Call, subscriber.IsRoaming, false); // Assuming non-peak for simplicity
        var dataTariff = await _tariffService.FindTariffAsync(UsageType.Data, subscriber.IsRoaming, false);
        var smsTariff = await _tariffService.FindTariffAsync(UsageType.SMS, subscriber.IsRoaming, false);

        bool isCallOverage = callLeft == 0;
        bool isDataOverage = dataLeft == 0;
        bool isSmsOverage = smsLeft == 0;

        // 6. Compose summary DTO
        var summary = new SubscriberUsageSummaryResponse
        {
            SubscriberId = subscriber.Id,
            SubscriberPhone = subscriber.PhoneNumber,
            PlanName = plan.Name,

            UsedCallMinutes = usedCallMinutes,
            UsedDataMB = usedDataMB,
            UsedSmsCount = usedSmsCount,

            CallMinutesBundle = plan.IncludedCallMinutes,
            DataBundleMB = plan.IncludedDataMB,
            SmsBundle = plan.IncludedSMS,

            CallMinutesLeft = callLeft,
            DataMBLeft = dataLeft,
            SmsLeft = smsLeft,

            CallMinutesOverBundle = callOverBundle,
            DataMBOverBundle = dataOverBundle,
            SmsOverBundle = smsOverBundle,

            PeakCalls = peakCalls,
            OffPeakCalls = offPeakCalls,

            CurrentCallUnitPrice = isCallOverage ? callTariff.PricePerUnit * 2 : callTariff.PricePerUnit,
            CurrentDataUnitPrice = isDataOverage ? dataTariff.PricePerUnit * 2 : dataTariff.PricePerUnit,
            CurrentSmsUnitPrice = isSmsOverage ? smsTariff.PricePerUnit * 2 : smsTariff.PricePerUnit,

            OverageCallUnitPrice = callTariff.PricePerUnit * 2,
            OverageDataUnitPrice = dataTariff.PricePerUnit * 2,
            OverageSmsUnitPrice = smsTariff.PricePerUnit * 2,

            IsCallOverage = isCallOverage,
            IsDataOverage = isDataOverage,
            IsSmsOverage = isSmsOverage,

            PeriodStart = periodStart,
            PeriodEnd = periodEnd,
        };

        return summary;
    }
}