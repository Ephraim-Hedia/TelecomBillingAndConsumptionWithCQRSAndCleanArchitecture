using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.UsageFeatures.Commands.Models;
using TelecomBillingAndConsumption.Core.Resources;
using TelecomBillingAndConsumption.Data.Entities;
using TelecomBillingAndConsumption.Service.Interfaces;

namespace TelecomBillingAndConsumption.Core.Features.UsageFeatures.Commands.Handlers
{
    public class UsageCommandHandler : ResponseHandler,
        IRequestHandler<AddUsageRecordCommand, Response<int>>,
        IRequestHandler<DeleteUsageRecordByIdCommand, Response<string>>
    {
        #region Fields
        private readonly IUsageRecordService _usageRecordService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Constructors
        public UsageCommandHandler(
            IUsageRecordService usageRecordService,
            IMapper mapper,
            IStringLocalizer<SharedResources> stringLocalizer

            ) : base(stringLocalizer)
        {
            _usageRecordService = usageRecordService;
            _mapper = mapper;
            _localizer = stringLocalizer;

        }
        #endregion
        #region Handle Functions
        public async Task<Response<int>> Handle(AddUsageRecordCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<UsageRecord>(request);
            var id = await _usageRecordService.AddAsync(entity);
            return id > 0
                ? Created(id)
                : BadRequest<int>(_localizer[SharedResourcesKeys.BadRequest]);
        }

        public async Task<Response<string>> Handle(DeleteUsageRecordByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await _usageRecordService.DeleteAsync(request.Id);
            return result
                ? Deleted<string>(_localizer[SharedResourcesKeys.Deleted])
                : NotFound<string>(_localizer[SharedResourcesKeys.NotFound]);
        }
        #endregion

    }
}
