using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.UsageFeatures.Queries.Models;
using TelecomBillingAndConsumption.Core.Features.UsageFeatures.Queries.Results;
using TelecomBillingAndConsumption.Core.Resources;
using TelecomBillingAndConsumption.Core.Wrappers;
using TelecomBillingAndConsumption.Service.Interfaces;

namespace TelecomBillingAndConsumption.Core.Features.UsageFeatures.Queries.Handlers
{
    public class UsageQueryHandler : ResponseHandler
        , IRequestHandler<GetUsageRecordByIdQuery, Response<GetUsageRecordByIdResponse>>
        , IRequestHandler<GetUsageRecordsBySubscriberIdQuery, PaginatedResult<GetUsageRecordsBySubscriberIdResponse>>
        , IRequestHandler<GetUsageSummaryBySubscriberIdQuery, PaginatedResult<GetUsageSummaryBySubscriberIdResponse>>
    {
        #region Fields
        private readonly IUsageRecordService _usageRecordService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public UsageQueryHandler(
            IUsageRecordService usageRecordService,
            IMapper mapper,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _mapper = mapper;
            _usageRecordService = usageRecordService;
            _localizer = stringLocalizer;
        }
        #endregion

        #region Handle Functions


        public async Task<Response<GetUsageRecordByIdResponse>> Handle(GetUsageRecordByIdQuery request, CancellationToken cancellationToken)
        {
            var usageRecord = await _usageRecordService.GetByIdAsync(request.Id);
            if (usageRecord == null)
                return NotFound<GetUsageRecordByIdResponse>(_localizer[SharedResourcesKeys.NotFound]);

            var response = _mapper.Map<GetUsageRecordByIdResponse>(usageRecord);
            return Success(response);
        }
        public async Task<PaginatedResult<GetUsageRecordsBySubscriberIdResponse>> Handle(GetUsageRecordsBySubscriberIdQuery request, CancellationToken cancellationToken)
        {
            var query = _usageRecordService.QueryUsageRecordsWithIncludes()
                .Where(r => r.SubscriberId == request.SubscriberId);

            var paginatedList = await _mapper
                .ProjectTo<GetUsageRecordsBySubscriberIdResponse>(query)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }



        public async Task<PaginatedResult<GetUsageSummaryBySubscriberIdResponse>> Handle(GetUsageSummaryBySubscriberIdQuery request, CancellationToken cancellationToken)
        {
            //var records = await _usageRecordService.GetBySubscriberAsync(request.SubscriberId);


            //var paginatedList = await _mapper
            //    .ProjectTo<GetUsageSummaryBySubscriberIdResponse>(records)
            //    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            //return paginatedList;

            throw new NotImplementedException();
        }


        #endregion
    }
}
