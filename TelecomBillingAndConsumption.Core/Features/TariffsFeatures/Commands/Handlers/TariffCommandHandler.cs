using MediatR;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.TariffsFeatures.Commands.Models;
using TelecomBillingAndConsumption.Core.Resources;

namespace TelecomBillingAndConsumption.Core.Features.TariffsFeatures.Commands.Handlers
{
    public class TariffCommandHandler : ResponseHandler,
        IRequestHandler<UpdateTariffRuleByIdCommand, Response<string>>,
        IRequestHandler<DeleteTariffRuleByIdCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Constructor
        public TariffCommandHandler(IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _localizer = stringLocalizer;
        }
        #endregion 

        #region Handle Functions
        public Task<Response<string>> Handle(UpdateTariffRuleByIdCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Response<string>> Handle(DeleteTariffRuleByIdCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

}
