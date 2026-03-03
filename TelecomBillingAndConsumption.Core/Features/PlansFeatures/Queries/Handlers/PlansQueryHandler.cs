using MediatR;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.PlansFeatures.Queries.Models;
using TelecomBillingAndConsumption.Core.Features.PlansFeatures.Queries.Results;
using TelecomBillingAndConsumption.Core.Resources;
using TelecomBillingAndConsumption.Core.Wrappers;

namespace TelecomBillingAndConsumption.Core.Features.PlansFeatures.Queries.Handlers
{
    public class PlansQueryHandler : ResponseHandler,
        IRequestHandler<GetAllPlansPaginatedQuery, PaginatedResult<GetAllPlansPaginatedResponse>>,
        IRequestHandler<GetPlanByIdQuery, Response<GetPlanByIdResponse>>
    {

        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Constructors
        public PlansQueryHandler(IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _localizer = localizer;
        }

        #endregion
        #region Handle Functions
        public Task<PaginatedResult<GetAllPlansPaginatedResponse>> Handle(GetAllPlansPaginatedQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Response<GetPlanByIdResponse>> Handle(GetPlanByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
