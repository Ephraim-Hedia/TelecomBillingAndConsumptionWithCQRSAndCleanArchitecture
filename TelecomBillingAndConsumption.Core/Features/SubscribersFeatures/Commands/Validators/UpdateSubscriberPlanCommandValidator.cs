using FluentValidation;
using TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Commands.Models;

namespace TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Commands.Validators
{
    public class UpdateSubscriberPlanCommandValidator : AbstractValidator<UpdateSubscriberPlanCommand>
    {
        public UpdateSubscriberPlanCommandValidator()
        {
            RuleFor(x => x.SubscriberId).GreaterThan(0).WithMessage("Subscriber Id must be greater than 0.");
            RuleFor(x => x.NewPlanId).GreaterThan(0).WithMessage("Plan Id must be greater than 0.");
        }
    }
}
