using FluentValidation;
using TelecomBillingAndConsumption.Core.Features.UsageFeatures.Commands.Models;

namespace TelecomBillingAndConsumption.Core.Features.UsageFeatures.Commands.Validators
{
    public class DeleteUsageRecordByIdValidator : AbstractValidator<DeleteUsageRecordByIdCommand>
    {
        public DeleteUsageRecordByIdValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("UsageRecord Id must be greater than 0.");
        }
    }
}
