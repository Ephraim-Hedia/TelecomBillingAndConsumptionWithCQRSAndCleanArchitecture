using FluentValidation;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Features.TariffsFeatures.Commands.Models;
using TelecomBillingAndConsumption.Core.Resources;

namespace TelecomBillingAndConsumption.Core.Features.TariffsFeatures.Commands.Validators
{
    public class UpdateTariffRuleByIdValidator : AbstractValidator<UpdateTariffRuleByIdCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Constructors
        public UpdateTariffRuleByIdValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _localizer = stringLocalizer;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Handle Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
            RuleFor(x => x.PricePerUnit)
                .GreaterThan(0).WithMessage("Price per unit must be greater than 0.");
            RuleFor(x => x.EffectiveFrom)
                .LessThan(DateTime.Now).WithMessage("Effective from date must be in the past.");
            RuleFor(x => x.UsageType)
                .IsInEnum().WithMessage("Invalid usage type.");
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
