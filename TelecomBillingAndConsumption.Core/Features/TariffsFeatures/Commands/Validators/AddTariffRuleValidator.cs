using FluentValidation;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Features.TariffsFeatures.Commands.Models;
using TelecomBillingAndConsumption.Core.Resources;

namespace TelecomBillingAndConsumption.Core.Features.TariffsFeatures.Commands.Validators
{
    public class AddTariffRuleValidator : AbstractValidator<AddTariffRuleCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Constructors
        public AddTariffRuleValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _localizer = stringLocalizer;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Handle Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.PricePerUnit)
                .GreaterThanOrEqualTo(0)
                .WithMessage(_localizer[SharedResourcesKeys.Required] + " Price must be non-negative.");
            RuleFor(x => x.UsageType)
                .IsInEnum()
                .WithMessage("Invalid Usage Type.");

            RuleFor(x => x.IsPeak)
                .NotNull().WithMessage("IsPeak must be specified.");

            RuleFor(x => x.IsRoaming)
                .NotNull().WithMessage("IsRoaming must be specified.");
        }

        public void ApplyCustomValidationsRules()
        {
        }

        #endregion
    }
}
