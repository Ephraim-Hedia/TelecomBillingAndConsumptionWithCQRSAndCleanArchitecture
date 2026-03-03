using FluentValidation;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Commands.Models;
using TelecomBillingAndConsumption.Core.Resources;

namespace TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Commands.Validators
{
    public class AddSubscriberValidator : AbstractValidator<AddSubscriberCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Constructors
        public AddSubscriberValidator(IStringLocalizer<SharedResources> stringLocalizer)
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
            RuleFor(x => x.PhoneNumber)
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(11).WithMessage("Phone Number Max Length is 11 Char.");
            RuleFor(x => x.Country)
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(50).WithMessage("Country Max Length is 50 Char.");
            RuleFor(x => x.PlanId)
                .GreaterThan(0).WithMessage("Plan Id must be greater than 0.");
        }

        public void ApplyCustomValidationsRules()
        {
        }

        #endregion
    }
}
