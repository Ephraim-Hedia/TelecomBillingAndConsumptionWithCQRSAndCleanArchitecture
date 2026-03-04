using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.TariffsFeatures.Queries.Models;
using TelecomBillingAndConsumption.Core.Features.TariffsFeatures.Queries.Results;
using TelecomBillingAndConsumption.Core.Resources;
using TelecomBillingAndConsumption.Service.Interfaces;

namespace TelecomBillingAndConsumption.Core.Features.TariffsFeatures.Queries.Handlers
{
    public class TariffQueryHandler : ResponseHandler,
        IRequestHandler<GetTariffRuleByIdQuery, Response<GetTariffRuleByIdResponse>>,
        IRequestHandler<GetAllTariffRulesQuery, List<GetAllTariffRulesResponse>>

    {
        #region Fields
        private readonly ITariffService _tariffService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public TariffQueryHandler(
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
        public async Task<List<GetAllTariffRulesResponse>> Handle(GetAllTariffRulesQuery request, CancellationToken cancellationToken)
        {
            var tariffs = await _tariffService.GetAllAsync();
            return _mapper.Map<List<GetAllTariffRulesResponse>>(tariffs);
        }

        public async Task<Response<GetTariffRuleByIdResponse>> Handle(GetTariffRuleByIdQuery request, CancellationToken cancellationToken)
        {
            // Returns exactly your desired table view
            var tariff = await _tariffService.GetByIdAsync(request.Id);
            if (tariff == null)
                return NotFound<GetTariffRuleByIdResponse>(_localizer[SharedResourcesKeys.NotFound]);

            var response = _mapper.Map<GetTariffRuleByIdResponse>(tariff);
            return Success(response);
        }
        #endregion
    }
}
