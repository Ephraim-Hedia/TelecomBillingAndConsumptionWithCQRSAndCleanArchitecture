using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.PlansFeatures.Commands.Models;
using TelecomBillingAndConsumption.Core.Resources;
using TelecomBillingAndConsumption.Data.Entities;
using TelecomBillingAndConsumption.Service.Interfaces.PlanService;

namespace TelecomBillingAndConsumption.Core.Features.PlansFeatures.Commands.Handlers
{
    public class PlansCommandHandler : ResponseHandler,
        IRequestHandler<AddPlanCommand, Response<int>>,
        IRequestHandler<UpdatePlanByIdCommand, Response<string>>,
        IRequestHandler<DeletePlanByIdCommand, Response<string>>
    {
        #region Fields
        private readonly IPlanService _planService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public PlansCommandHandler(
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
        public async Task<Response<int>> Handle(AddPlanCommand request, CancellationToken cancellationToken)
        {
            var plan = _mapper.Map<Plan>(request);
            var id = await _planService.AddAsync(plan);
            return id > 0 ? Created(id) : BadRequest<int>(_localizer[SharedResourcesKeys.BadRequest]);
        }

        public async Task<Response<string>> Handle(UpdatePlanByIdCommand request, CancellationToken cancellationToken)
        {
            var existingPlan = await _planService.GetByIdAsync(request.Id);
            if (existingPlan == null)
                return NotFound<string>(_localizer[SharedResourcesKeys.NotFound]);

            _mapper.Map(request, existingPlan);
            await _planService.UpdateAsync(existingPlan);
            return Updated<string>(_localizer[SharedResourcesKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeletePlanByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await _planService.DeleteAsync(request.Id);
            return result
                ? Deleted<string>(_localizer[SharedResourcesKeys.Deleted])
                : NotFound<string>(_localizer[SharedResourcesKeys.NotFound]);
        }
        #endregion
    }

}
