using FluentValidation;
using TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Commands.Models;
using TelecomBillingAndConsumption.Service.Interfaces.PlanService;

namespace TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Commands.Validators
{
    public class UpdateSubscriberByIdValidator : AbstractValidator<UpdateSubscriberByIdCommand>
    {
        private readonly IPlanService _planService;

        public UpdateSubscriberByIdValidator(IPlanService planService)
        {
            _planService = planService;
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Subscriber Id must be greater than 0.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Subscriber name is required.")
                .MaximumLength(50);

            RuleFor(x => x.Country)
                .NotEmpty()
                .WithMessage("Country is required.");

            RuleFor(x => x.PlanId)
                .GreaterThan(0)
                .WithMessage("PlanId must be greater than 0.")
                .MustAsync(async (planId, cancellation) =>
                {
                    var plan = await _planService.GetByIdAsync(planId);
                    return plan != null;
                })
                .WithMessage("Selected plan does not exist.");
        }
    }
}
