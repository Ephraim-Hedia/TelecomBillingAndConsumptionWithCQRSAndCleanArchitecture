using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Models;
using TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Results;
using TelecomBillingAndConsumption.Core.Resources;
using TelecomBillingAndConsumption.Core.Wrappers;
using TelecomBillingAndConsumption.Service.Interfaces;

namespace TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Handlers
{
    public class BillingQueryHandler : ResponseHandler,
        IRequestHandler<GetBillByIdQuery, Response<GetBillByIdResponse>>,
        IRequestHandler<GetBillingDetailsByBillIdQuery, List<GetBillingDetailsByBillIdResponse>>,
        IRequestHandler<GetAllBillingsBySubscriberIdQuery, PaginatedResult<GetAllBillingsBySubscriberIdResponse>>,
        IRequestHandler<GetBillBySubscriberIdAndMonthQuery, Response<GetBillBySubscriberIdAndMonthResponse>>


    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IBillService _billService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor

        public BillingQueryHandler(
            IBillService billService,
            IMapper mapper,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _mapper = mapper;
            _billService = billService;
            _localizer = stringLocalizer;
        }
        #endregion

        #region Handlers
        public async Task<Response<GetBillByIdResponse>> Handle(GetBillByIdQuery request, CancellationToken cancellationToken)
        {
            var bill = await _billService.GetBillAsync(request.BillId);
            if (bill == null)
            {
                return NotFound<GetBillByIdResponse>(_localizer[SharedResourcesKeys.NotFound]);
            }
            var mappedbill = _mapper.Map<GetBillByIdResponse>(bill);
            return Success(mappedbill);
        }



        public Task<List<GetBillingDetailsByBillIdResponse>> Handle(GetBillingDetailsByBillIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginatedResult<GetAllBillingsBySubscriberIdResponse>> Handle(GetAllBillingsBySubscriberIdQuery request, CancellationToken cancellationToken)
        {
            var billsQuarable = _billService.GetAllBillsBySubsriberIdQuarable(request.SubscriberId);
            var paginatedList = await _mapper.ProjectTo<GetAllBillingsBySubscriberIdResponse>(billsQuarable)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }

        public async Task<Response<GetBillBySubscriberIdAndMonthResponse>> Handle(GetBillBySubscriberIdAndMonthQuery request, CancellationToken cancellationToken)
        {
            var bill = await _billService.GetAllBillsBySubsriberIdAndMonthAsync(request.SubscriberId, request.Month);
            if (bill == null)
            {
                return NotFound<GetBillBySubscriberIdAndMonthResponse>(_localizer[SharedResourcesKeys.NotFound]);
            }
            var mappedbill = _mapper.Map<GetBillBySubscriberIdAndMonthResponse>(bill);
            return Success(mappedbill);
        }
        #endregion

    }
}
