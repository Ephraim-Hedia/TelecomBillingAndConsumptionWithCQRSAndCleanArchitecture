using FluentValidation;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Features.PlansFeatures.Commands.Models;
using TelecomBillingAndConsumption.Core.Resources;

namespace TelecomBillingAndConsumption.Core.Features.PlansFeatures.Commands.Validators
{
    public class DeactivatePlanByIdValidator : AbstractValidator<DeactivatePlanByIdCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Constructors
        public DeactivatePlanByIdValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage(_localizer[SharedResourcesKeys.Required] + " Plan Id must be greater than zero.");
        }
        #endregion
    }
}
