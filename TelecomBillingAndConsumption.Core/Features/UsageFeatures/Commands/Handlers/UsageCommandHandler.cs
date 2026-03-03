using MediatR;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.UsageFeatures.Commands.Models;
using TelecomBillingAndConsumption.Core.Resources;

namespace TelecomBillingAndConsumption.Core.Features.UsageFeatures.Commands.Handlers
{
    public class UsageCommandHandler : ResponseHandler,
        IRequestHandler<AddUsageRecordCommand, Response<int>>,
        IRequestHandler<DeleteUsageRecordByIdCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Constructors
        public UsageCommandHandler(IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _localizer = stringLocalizer;
        }
        #endregion
        #region Handle Functions
        public async Task<Response<int>> Handle(AddUsageRecordCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async Task<Response<string>> Handle(DeleteUsageRecordByIdCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
