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
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required])
                .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthis100]);

            RuleFor(x => x.MonthlyFee)
                .GreaterThanOrEqualTo(0).WithMessage(_localizer[SharedResourcesKeys.Required] + " Monthly Fee cannot be negative.");

            RuleFor(x => x.IncludedCallMinutes)
                .GreaterThanOrEqualTo(0).WithMessage(_localizer[SharedResourcesKeys.Required] + " Included Call Minutes cannot be negative.");

            RuleFor(x => x.IncludedDataMB)
                .GreaterThanOrEqualTo(0).WithMessage(_localizer[SharedResourcesKeys.Required] + " Included Data MB cannot be negative.");

            RuleFor(x => x.IncludedSMS)
                .GreaterThanOrEqualTo(0).WithMessage(_localizer[SharedResourcesKeys.Required] + " Included SMS cannot be negative.");
        }

        public void ApplyCustomValidationsRules()
        {

        }
        #endregion
    }
}
