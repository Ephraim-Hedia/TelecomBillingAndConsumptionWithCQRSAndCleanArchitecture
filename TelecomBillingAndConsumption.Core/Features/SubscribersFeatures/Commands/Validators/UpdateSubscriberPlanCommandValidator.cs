using FluentValidation;
using TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Commands.Models;
using TelecomBillingAndConsumption.Service.Interfaces;
using TelecomBillingAndConsumption.Service.Interfaces.PlanService;

namespace TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Commands.Validators
{
    public class UpdateSubscriberPlanCommandValidator : AbstractValidator<UpdateSubscriberPlanCommand>
    {
        private readonly ISubscriberService _subscriberService;
        private readonly IPlanService _planService;

        public UpdateSubscriberPlanCommandValidator(
            ISubscriberService subscriberService,
            IPlanService planService)
        {
            _subscriberService = subscriberService;
            _planService = planService;

            RuleFor(x => x.SubscriberId)
                .GreaterThan(0)
                .WithMessage("SubscriberId must be greater than 0.");

            RuleFor(x => x.NewPlanId)
                .GreaterThan(0)
                .WithMessage("NewPlanId must be greater than 0.")
                .MustAsync(PlanExists)
                .WithMessage("The selected plan does not exist.");

            RuleFor(x => x)
                .MustAsync(NotSamePlan)
                .WithMessage("Subscriber is already assigned to this plan.");
        }

        private async Task<bool> PlanExists(int planId, CancellationToken cancellationToken)
        {
            var plan = await _planService.GetByIdAsync(planId);
            return plan != null;
        }

        private async Task<bool> NotSamePlan(UpdateSubscriberPlanCommand command, CancellationToken cancellationToken)
        {
            var subscriber = await _subscriberService.GetByIdAsync(command.SubscriberId);

            if (subscriber == null)
                return false;

            return subscriber.PlanId != command.NewPlanId;
        }
    }
}
