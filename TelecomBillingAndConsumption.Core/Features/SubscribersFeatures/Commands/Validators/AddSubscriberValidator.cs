using FluentValidation;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Commands.Models;
using TelecomBillingAndConsumption.Core.Resources;
using TelecomBillingAndConsumption.Service.Interfaces;
using TelecomBillingAndConsumption.Service.Interfaces.PlanService;

namespace TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Commands.Validators
{
    public class AddSubscriberValidator : AbstractValidator<AddSubscriberCommand>
    {
        private readonly ISubscriberService _subscriberService;
        private readonly IPlanService _planService;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public AddSubscriberValidator(
            ISubscriberService subscriberService,
            IPlanService planService,
            IStringLocalizer<SharedResources> stringLocalizer)
        {
            _subscriberService = subscriberService;
            _planService = planService;
            _localizer = stringLocalizer;

            ApplyValidationRules();
            ApplyCustomRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Subscriber name is required.")
                .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^01[0-9]{9}$")
                .WithMessage("Phone number must be a valid Egyptian mobile number.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(50).WithMessage("Country cannot exceed 50 characters.");

            RuleFor(x => x.PlanId)
                .GreaterThan(0).WithMessage("PlanId must be greater than 0.");
        }

        private void ApplyCustomRules()
        {
            RuleFor(x => x.PhoneNumber)
                .MustAsync(async (phone, cancellation) =>
                {
                    return !await _subscriberService.ExistsByPhoneAsync(phone);
                })
                .WithMessage("Phone number already exists.");

            RuleFor(x => x.PlanId)
                .MustAsync(async (planId, cancellation) =>
                {
                    var plan = await _planService.GetByIdAsync(planId);
                    return plan != null;
                })
                .WithMessage("Selected plan does not exist.");
        }
    }
}
