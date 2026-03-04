using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.TariffsFeatures.Commands.Models;
using TelecomBillingAndConsumption.Core.Resources;
using TelecomBillingAndConsumption.Data.Entities;
using TelecomBillingAndConsumption.Service.Interfaces;

namespace TelecomBillingAndConsumption.Core.Features.TariffsFeatures.Commands.Handlers
{
    public class TariffCommandHandler : ResponseHandler,
        IRequestHandler<AddTariffRuleCommand, Response<int>>,
        IRequestHandler<UpdateTariffRuleByIdCommand, Response<string>>,
        IRequestHandler<DeleteTariffRuleByIdCommand, Response<string>>
    {
        #region Fields
        private readonly ITariffService _tariffService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Constructors
        public TariffCommandHandler(
            ITariffService tariffService,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer
        ) : base(localizer)
        {
            _tariffService = tariffService;
            _mapper = mapper;
            _localizer = localizer;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<int>> Handle(AddTariffRuleCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TariffRule>(request);
            var id = await _tariffService.AddAsync(entity);
            return id > 0
                ? Created(id)
                : BadRequest<int>(_localizer[SharedResourcesKeys.BadRequest]);
        }

        public async Task<Response<string>> Handle(UpdateTariffRuleByIdCommand request, CancellationToken cancellationToken)
        {
            var existing = await _tariffService.GetByIdAsync(request.Id);
            if (existing == null)
                return NotFound<string>(_localizer[SharedResourcesKeys.NotFound]);

            _mapper.Map(request, existing); // Map updates onto entity
            var result = await _tariffService.UpdateAsync(existing);

            return result
                ? Updated<string>(_localizer[SharedResourcesKeys.Updated])
                : BadRequest<string>(_localizer[SharedResourcesKeys.BadRequest]);
        }

        public async Task<Response<string>> Handle(DeleteTariffRuleByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await _tariffService.DeleteAsync(request.Id);
            return result
                ? Deleted<string>(_localizer[SharedResourcesKeys.Deleted])
                : NotFound<string>(_localizer[SharedResourcesKeys.NotFound]);
        }
        #endregion
    }

}
