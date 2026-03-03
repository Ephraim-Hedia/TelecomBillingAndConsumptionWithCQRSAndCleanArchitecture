using MediatR;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.BillingFeatures.Commands.Models;
using TelecomBillingAndConsumption.Core.Resources;

namespace TelecomBillingAndConsumption.Core.Features.BillingFeatures.Commands.Handlers
{
    public class BillingCommandHandler : ResponseHandler,
        IRequestHandler<AddBillingToSubscriberIdCommand, Response<int>>
    {
        #region Fields  
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructor
        public BillingCommandHandler(IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _localizer = stringLocalizer;
        }
        #endregion

        #region Handlers
        public Task<Response<int>> Handle(AddBillingToSubscriberIdCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
