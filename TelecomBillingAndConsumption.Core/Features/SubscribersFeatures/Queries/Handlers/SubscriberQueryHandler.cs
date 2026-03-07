using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Queries.Models;
using TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Queries.Results;
using TelecomBillingAndConsumption.Core.Resources;
using TelecomBillingAndConsumption.Core.Wrappers;
using TelecomBillingAndConsumption.Service.Interfaces;
using TelecomBillingAndConsumption.Service.Interfaces.PlanService;

namespace TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Queries.Handlers
{
    public class SubscriberQueryHandler : ResponseHandler,
        IRequestHandler<GetAllSubscribersPaginatedQuery, PaginatedResult<GetAllSubscribersPaginatedResponse>>,
        IRequestHandler<GetSubscriberByIdQuery, Response<GetSubscriberByIdResponse>>,
        IRequestHandler<GetSubscriberUsageSummaryQuery, Response<SubscriberUsageSummaryResponse>>
    {
        #region Fields
        private readonly ISubscriberService _subscriberService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IPlanService _planService;
        private readonly IUsageRecordService _usageRecordService;
        private readonly ITariffService _tariffService;
        private readonly IUsageSummaryService _usageSummaryService;
        #endregion

        #region Constructors
        public SubscriberQueryHandler(
            IUsageSummaryService usageSummaryService,
            ISubscriberService subscriberService,
            IUsageRecordService usageRecordService,
            IPlanService planService,
            ITariffService tariffService,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer
        ) : base(localizer)
        {
            _usageSummaryService = usageSummaryService;
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

        public async Task<Response<SubscriberUsageSummaryResponse>> Handle(GetSubscriberUsageSummaryQuery request, CancellationToken cancellationToken)
        {
            var summary = await _usageSummaryService.GetUsageSummaryAsync(request.SubscriberId, DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow);
            if (summary == null)
                return NotFound<SubscriberUsageSummaryResponse>(_localizer[SharedResourcesKeys.NotFound]);
            var response = _mapper.Map<SubscriberUsageSummaryResponse>(summary);
            return Success(response);
        }
        #endregion
    }
}
