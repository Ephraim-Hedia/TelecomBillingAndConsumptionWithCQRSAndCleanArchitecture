using MediatR;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.BillingFeatures.Commands.Models;
using TelecomBillingAndConsumption.Core.Resources;
using TelecomBillingAndConsumption.Service.Interfaces;

namespace TelecomBillingAndConsumption.Core.Features.BillingFeatures.Commands.Handlers
{
    public class BillingCommandHandler : ResponseHandler,
        IRequestHandler<AddBillingToSubscriberIdCommand, Response<int>>
    {
        #region Fields  
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IBillService _billService;
        #endregion

        #region Constructor
        public BillingCommandHandler(
            IStringLocalizer<SharedResources> stringLocalizer,
            IBillService billService) : base(stringLocalizer)
        {
            _billService = billService;
            _localizer = stringLocalizer;

        }
        #endregion

        #region Handlers
        public async Task<Response<int>> Handle(AddBillingToSubscriberIdCommand request, CancellationToken cancellationToken)
        {
            int billId = await _billService.GenerateMonthlyBillAsync(request.SubscriberId, request.Month);
            return Success(billId);
        }
        #endregion
    }
}
