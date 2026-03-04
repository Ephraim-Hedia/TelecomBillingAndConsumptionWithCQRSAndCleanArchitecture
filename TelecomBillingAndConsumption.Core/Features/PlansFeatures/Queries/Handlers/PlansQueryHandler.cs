using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.PlansFeatures.Queries.Models;
using TelecomBillingAndConsumption.Core.Features.PlansFeatures.Queries.Results;
using TelecomBillingAndConsumption.Core.Resources;
using TelecomBillingAndConsumption.Core.Wrappers;
using TelecomBillingAndConsumption.Service.Interfaces.PlanService;

namespace TelecomBillingAndConsumption.Core.Features.PlansFeatures.Queries.Handlers
{
    public class PlansQueryHandler : ResponseHandler,
        IRequestHandler<GetAllPlansPaginatedQuery, PaginatedResult<GetAllPlansPaginatedResponse>>,
        IRequestHandler<GetPlanByIdQuery, Response<GetPlanByIdResponse>>
    {

        #region Fields
        private readonly IPlanService _planService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public PlansQueryHandler(
            IPlanService planService,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer
        ) : base(localizer)
        {
            _planService = planService;
            _mapper = mapper;
            _localizer = localizer;
        }
        #endregion


        #region Handle Functions
        public async Task<PaginatedResult<GetAllPlansPaginatedResponse>> Handle(GetAllPlansPaginatedQuery request, CancellationToken cancellationToken)
        {
            var plansQuery = _planService.GetPlansAsQuarable(); // Should return IQueryable<Plan>
            var paginatedList = await _mapper
                .ProjectTo<GetAllPlansPaginatedResponse>(plansQuery)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }

        public async Task<Response<GetPlanByIdResponse>> Handle(GetPlanByIdQuery request, CancellationToken cancellationToken)
        {
            var plan = await _planService.GetByIdAsync(request.Id);
            if (plan == null)
                return NotFound<GetPlanByIdResponse>(_localizer[SharedResourcesKeys.NotFound]);
            var response = _mapper.Map<GetPlanByIdResponse>(plan);
            return Success(response);
        }
        #endregion

    }
}
