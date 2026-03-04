using FluentValidation;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Commands.Models;
using TelecomBillingAndConsumption.Core.Resources;
using TelecomBillingAndConsumption.Service.Interfaces;

namespace TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Commands.Validators
{
    public class AddSubscriberValidator : AbstractValidator<AddSubscriberCommand>
    {
        #region Fields
        private readonly ISubscriberService _subscriberService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Constructors
        public AddSubscriberValidator(
            ISubscriberService subscriberService, IStringLocalizer<SharedResources> stringLocalizer)
        {
            _subscriberService = subscriberService;
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
                .MaximumLength(11).WithMessage("Phone Number Max Length is 11 Char.")
                ;
            RuleFor(x => x.Country)
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(50).WithMessage("Country Max Length is 50 Char.");
            RuleFor(x => x.PlanId)
                .GreaterThan(0).WithMessage("Plan Id must be greater than 0.");


        }

        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required] + " Phone is required.")
            .MustAsync(async (phone, cancellation) =>
            {
                return !await _subscriberService.ExistsByPhoneAsync(phone);
            })
            .WithMessage(_localizer[SharedResourcesKeys.PhoneNumber] + " already exists.");
            // ... other rules as needed
        }

        #endregion
    }
}
