using FluentValidation;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Features.PlansFeatures.Commands.Models;
using TelecomBillingAndConsumption.Core.Resources;

namespace TelecomBillingAndConsumption.Core.Features.PlansFeatures.Commands.Validators
{
    public class AddPlanValidator : AbstractValidator<AddPlanCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Constructors
        public AddPlanValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _localizer = stringLocalizer;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion


        #region Handle Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(50).WithMessage("Name Max Length is 50 Char.");
            RuleFor(x => x.IncludedCallMinutes)
                .GreaterThanOrEqualTo(0).WithMessage("Included Call Minutes must be greater than or equal to 0.");
            RuleFor(x => x.IncludedDataMB)
                .GreaterThanOrEqualTo(0).WithMessage("Included Data MB must be greater than or equal to 0.");
            RuleFor(x => x.IncludedSMS)
                .GreaterThanOrEqualTo(0).WithMessage("Included SMS must be greater than or equal to 0.");
            RuleFor(x => x.MonthlyFee)
                .GreaterThanOrEqualTo(0).WithMessage("Monthly Fee must be greater than or equal to 0.");
        }

        public void ApplyCustomValidationsRules()
        {

        }
        #endregion
    }
}
