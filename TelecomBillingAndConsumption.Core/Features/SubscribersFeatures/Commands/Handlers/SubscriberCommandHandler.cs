using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Commands.Models;
using TelecomBillingAndConsumption.Core.Resources;
using TelecomBillingAndConsumption.Data.Entities;
using TelecomBillingAndConsumption.Service.Interfaces;

namespace TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Commands.Handlers
{
    public class SubscriberCommandHandler : ResponseHandler,
        IRequestHandler<AddSubscriberCommand, Response<int>>,
        IRequestHandler<UpdateSubscriberByIdCommand, Response<string>>,
        IRequestHandler<DeleteSubscriberByIdCommand, Response<string>>,
        IRequestHandler<ActivateSubscriberByIdCommand, Response<string>>,
        IRequestHandler<DeactivateSubscriberByIdCommand, Response<string>>,
        IRequestHandler<UpdateSubscriberPlanCommand, Response<string>>
    {
        #region Fields
        private readonly ISubscriberService _subscriberService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public SubscriberCommandHandler(
            ISubscriberService subscriberService,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer
        ) : base(localizer)
        {
            _subscriberService = subscriberService;
            _mapper = mapper;
            _localizer = localizer;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<int>> Handle(AddSubscriberCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Subscriber>(request);
            var id = await _subscriberService.AddAsync(entity);
            return id > 0
                ? Created(id)
                : BadRequest<int>(_localizer[SharedResourcesKeys.BadRequest]);
        }

        public async Task<Response<string>> Handle(UpdateSubscriberByIdCommand request, CancellationToken cancellationToken)
        {
            var existing = await _subscriberService.GetByIdAsync(request.Id);
            if (existing == null)
                return NotFound<string>(_localizer[SharedResourcesKeys.NotFound]);

            _mapper.Map(request, existing);
            var updated = await _subscriberService.UpdateAsync(existing);

            return updated
                ? Updated<string>(_localizer[SharedResourcesKeys.Updated])
                : BadRequest<string>(_localizer[SharedResourcesKeys.BadRequest]);
        }

        public async Task<Response<string>> Handle(DeleteSubscriberByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await _subscriberService.DeleteAsync(request.Id);
            return result
                ? Deleted<string>(_localizer[SharedResourcesKeys.Deleted])
                : NotFound<string>(_localizer[SharedResourcesKeys.NotFound]);
        }

        public async Task<Response<string>> Handle(ActivateSubscriberByIdCommand request, CancellationToken cancellationToken)
        {
            var success = await _subscriberService.ActivateAsync(request.Id);
            return success
                ? Success("Subscriber activated successfully.")
                : NotFound<string>(_localizer[SharedResourcesKeys.NotFound]);
        }

        public async Task<Response<string>> Handle(DeactivateSubscriberByIdCommand request, CancellationToken cancellationToken)
        {
            var success = await _subscriberService.DeactivateAsync(request.Id);
            return success
                ? Success("Subscriber deactivated successfully.")
                : NotFound<string>(_localizer[SharedResourcesKeys.NotFound]);
        }

        public async Task<Response<string>> Handle(UpdateSubscriberPlanCommand request, CancellationToken cancellationToken)
        {
            var success = await _subscriberService.UpdateSubscriberPlanAsync(request.SubscriberId, request.NewPlanId);
            if (!success)
                return NotFound<string>("Subscriber or Plan not found / update failed.");

            return Success("Plan updated successfully.");
        }
        #endregion
    }
}
