using FluentValidation;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Features.BillingFeatures.Commands.Models;
using TelecomBillingAndConsumption.Core.Resources;

namespace TelecomBillingAndConsumption.Core.Features.BillingFeatures.Commands.Validators
{
    public class AddBillingToSubscriberIdValidator : AbstractValidator<AddBillingToSubscriberIdCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Constructors
        public AddBillingToSubscriberIdValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _localizer = stringLocalizer;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion
        #region Handle Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.SubscriberId)
                .GreaterThan(0).WithMessage("Subscriber Id must be greater than 0.");
            RuleFor(x => x.Month)
                .InclusiveBetween(1, 12).WithMessage("Month must be between 1 and 12.");
            RuleFor(x => x.Year)
                .GreaterThan(0).WithMessage("Year must be greater than 0.");
        }

        public void ApplyCustomValidationsRules()
        {
        }
        #endregion
    }
}
