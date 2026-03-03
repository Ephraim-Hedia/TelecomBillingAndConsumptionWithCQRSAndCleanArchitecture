using MediatR;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.PlansFeatures.Commands.Models;
using TelecomBillingAndConsumption.Core.Resources;

namespace TelecomBillingAndConsumption.Core.Features.PlansFeatures.Commands.Handlers
{
    public class PlansCommandHandler : ResponseHandler,
        IRequestHandler<UpdatePlanByIdCommand, Response<string>>,
        IRequestHandler<DeletePlanByIdCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion


        #region Constructors
        public PlansCommandHandler(IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _localizer = localizer;
        }
        #endregion


        #region Handle Functions
        public Task<Response<string>> Handle(UpdatePlanByIdCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Response<string>> Handle(DeletePlanByIdCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion



    }
}
