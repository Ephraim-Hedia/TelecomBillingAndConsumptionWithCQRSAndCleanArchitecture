using MediatR;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.TariffsFeatures.Queries.Models;
using TelecomBillingAndConsumption.Core.Features.TariffsFeatures.Queries.Results;
using TelecomBillingAndConsumption.Core.Resources;

namespace TelecomBillingAndConsumption.Core.Features.TariffsFeatures.Queries.Handlers
{
    public class TariffQueryHandler : ResponseHandler,
        IRequestHandler<GetTariffRuleByIdQuery, Response<GetTariffRuleByIdResponse>>,
        IRequestHandler<GetAllTariffsRulesQuery, List<GetAllTariffsRulesResponse>>

    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructor
        public TariffQueryHandler(IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _localizer = stringLocalizer;
        }


        #endregion

        #region Handle Functions
        public Task<Response<GetTariffRuleByIdResponse>> Handle(GetTariffRuleByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetAllTariffsRulesResponse>> Handle(GetAllTariffsRulesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
