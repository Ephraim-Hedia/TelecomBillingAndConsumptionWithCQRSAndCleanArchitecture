using Microsoft.EntityFrameworkCore;
using TelecomBillingAndConsumption.Data.Entities;
using TelecomBillingAndConsumption.Infrastructure.InfrastructureBases;
using TelecomBillingAndConsumption.Service.Interfaces;
using TelecomBillingAndConsumption.Service.Interfaces.PlanService;

namespace TelecomBillingAndConsumption.Service.Implementation
{
    public class PlanLimitService : IPlanLimitService
    {
        private readonly IGenericRepository<UsageRecord> _usageRecordRepository;
        private readonly ISubscriberService _subscriberService;
        private readonly IPlanService _planService;

        public PlanLimitService(
            IGenericRepository<UsageRecord> usageRecordRepository,
            ISubscriberService subscriberService,
            IPlanService planService)
        {
            _usageRecordRepository = usageRecordRepository;
            _subscriberService = subscriberService;
            _planService = planService;
        }

        public async Task<bool> IsCallLimitExceededAsync(int subscriberId, DateTime usageTimestamp, int newCallMinutes)
        {
            var subscriber = await _subscriberService.GetByIdAsync(subscriberId);
            var plan = await _planService.GetByIdAsync(subscriber.PlanId);

            var monthStart = new DateTime(usageTimestamp.Year, usageTimestamp.Month, 1);
            var monthEnd = monthStart.AddMonths(1);

            int usedCalls = await _usageRecordRepository.GetTableNoTracking()
                .Where(r => r.SubscriberId == subscriberId
                    && r.Timestamp >= monthStart
                    && r.Timestamp < monthEnd)
                .SumAsync(r => r.CallMinutes ?? 0);

            return (usedCalls + newCallMinutes) > plan.IncludedCallMinutes;
        }

        public async Task<bool> IsDataLimitExceededAsync(int subscriberId, DateTime usageTimestamp, decimal newDataMB)
        {
            var subscriber = await _subscriberService.GetByIdAsync(subscriberId);
            var plan = await _planService.GetByIdAsync(subscriber.PlanId);

            var monthStart = new DateTime(usageTimestamp.Year, usageTimestamp.Month, 1);
            var monthEnd = monthStart.AddMonths(1);

            decimal usedData = await _usageRecordRepository.GetTableNoTracking()
                .Where(r => r.SubscriberId == subscriberId
                    && r.Timestamp >= monthStart
                    && r.Timestamp < monthEnd)
                .SumAsync(r => r.DataMB ?? 0);

            return (usedData + newDataMB) > plan.IncludedDataMB;
        }

        public async Task<bool> IsSmsLimitExceededAsync(int subscriberId, DateTime usageTimestamp, int newSmsCount)
        {
            var subscriber = await _subscriberService.GetByIdAsync(subscriberId);
            var plan = await _planService.GetByIdAsync(subscriber.PlanId);

            var monthStart = new DateTime(usageTimestamp.Year, usageTimestamp.Month, 1);
            var monthEnd = monthStart.AddMonths(1);

            int usedSms = await _usageRecordRepository.GetTableNoTracking()
                .Where(r => r.SubscriberId == subscriberId
                    && r.Timestamp >= monthStart
                    && r.Timestamp < monthEnd)
                .SumAsync(r => r.SMSCount ?? 0);

            return (usedSms + newSmsCount) > plan.IncludedSMS;
        }
    }
}
