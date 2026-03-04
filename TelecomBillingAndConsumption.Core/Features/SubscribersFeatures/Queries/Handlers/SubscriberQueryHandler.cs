using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Queries.Models;
using TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Queries.Results;
using TelecomBillingAndConsumption.Core.Resources;
using TelecomBillingAndConsumption.Core.Wrappers;
using TelecomBillingAndConsumption.Data.Helpers;
using TelecomBillingAndConsumption.Service.Interfaces;
using TelecomBillingAndConsumption.Service.Interfaces.PlanService;

namespace TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Queries.Handlers
{
    public class SubscriberQueryHandler : ResponseHandler,
        IRequestHandler<GetAllSubscribersPaginatedQuery, PaginatedResult<GetAllSubscribersPaginatedResponse>>,
        IRequestHandler<GetSubscriberByIdQuery, Response<GetSubscriberByIdResponse>>,
        IRequestHandler<GetSubscriberUsageSummaryQuery, SubscriberUsageSummaryResponse>
    {
        #region Fields
        private readonly ISubscriberService _subscriberService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IPlanService _planService;
        private readonly IUsageRecordService _usageRecordService;
        private readonly ITariffService _tariffService;
        #endregion

        #region Constructors
        public SubscriberQueryHandler(
            ISubscriberService subscriberService,
            IUsageRecordService usageRecordService,
            IPlanService planService,
            ITariffService tariffService,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer
        ) : base(localizer)
        {
            _tariffService = tariffService;
            _usageRecordService = usageRecordService;
            _planService = planService;
            _subscriberService = subscriberService;
            _mapper = mapper;
            _localizer = localizer;
        }
        #endregion

        #region Handle Functions
        public async Task<PaginatedResult<GetAllSubscribersPaginatedResponse>> Handle(GetAllSubscribersPaginatedQuery request, CancellationToken cancellationToken)
        {
            var query = _subscriberService.QuerySubscribers();
            var paginatedList = await _mapper
                .ProjectTo<GetAllSubscribersPaginatedResponse>(query)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }

        public async Task<Response<GetSubscriberByIdResponse>> Handle(GetSubscriberByIdQuery request, CancellationToken cancellationToken)
        {
            var subscriber = await _subscriberService.GetByIdAsync(request.Id);
            if (subscriber == null)
                return NotFound<GetSubscriberByIdResponse>(_localizer[SharedResourcesKeys.NotFound]);

            var response = _mapper.Map<GetSubscriberByIdResponse>(subscriber);
            return Success(response);
        }

        public async Task<SubscriberUsageSummaryResponse> Handle(GetSubscriberUsageSummaryQuery request, CancellationToken cancellationToken)
        {
            // Load subscriber and plan
            var subscriber = await _subscriberService.GetByIdAsync(request.SubscriberId);
            var plan = await _planService.GetByIdAsync(subscriber.PlanId);

            // Compute current period (month)
            var now = DateTime.UtcNow;
            var periodStart = new DateTime(now.Year, now.Month, 1);
            var periodEnd = periodStart.AddMonths(1).AddSeconds(-1);

            // Get all usage for this subscriber this billing period
            var usageRecords = await _usageRecordService.GetBySubscriberAsync(subscriber.Id);
            var usageInPeriod = usageRecords
                .Where(r => r.Timestamp >= periodStart && r.Timestamp < periodEnd)
                .ToList();

            // Aggregate totals
            int usedCallMinutes = usageInPeriod.Sum(r => r.CallMinutes ?? 0);
            decimal usedDataMB = usageInPeriod.Sum(r => r.DataMB ?? 0m);
            int usedSmsCount = usageInPeriod.Sum(r => r.SMSCount ?? 0);

            // Calculate left and overage
            int callLeft = Math.Max(0, plan.IncludedCallMinutes - usedCallMinutes);
            int callOverBundle = Math.Max(0, usedCallMinutes - plan.IncludedCallMinutes);

            decimal dataLeft = Math.Max(0, plan.IncludedDataMB - usedDataMB);
            decimal dataOverBundle = Math.Max(0, usedDataMB - plan.IncludedDataMB);

            int smsLeft = Math.Max(0, plan.IncludedSMS - usedSmsCount);
            int smsOverBundle = Math.Max(0, usedSmsCount - plan.IncludedSMS);

            // Get current tariffs
            var callTariff = await _tariffService.FindTariffAsync(UsageType.Call, subscriber.IsRoaming, false); // non-peak as default
            var dataTariff = await _tariffService.FindTariffAsync(UsageType.Data, subscriber.IsRoaming, false);
            var smsTariff = await _tariffService.FindTariffAsync(UsageType.SMS, subscriber.IsRoaming, false);

            // Detect overage status
            bool isCallOverage = callLeft == 0;
            bool isDataOverage = dataLeft == 0;
            bool isSmsOverage = smsLeft == 0;

            // Prepare response
            var response = new SubscriberUsageSummaryResponse
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

            return response;
        }
        #endregion
    }
}
