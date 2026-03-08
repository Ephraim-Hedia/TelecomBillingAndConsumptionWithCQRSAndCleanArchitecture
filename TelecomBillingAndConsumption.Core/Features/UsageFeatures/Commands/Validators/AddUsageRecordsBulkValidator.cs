using FluentValidation;
using TelecomBillingAndConsumption.Core.Features.UsageFeatures.Commands.Models;

namespace TelecomBillingAndConsumption.Core.Features.UsageFeatures.Commands.Validators
{
    public class AddUsageRecordsBulkValidator : AbstractValidator<AddUsageRecordsBulkCommand>
    {
        public AddUsageRecordsBulkValidator(IValidator<AddUsageRecordCommand> recordValidator)
        {
            RuleFor(x => x.Records)
                .NotNull()
                .WithMessage("Records collection cannot be null.")
                .NotEmpty()
                .WithMessage("At least one usage record must be provided.")
                .Must(r => r.Count <= 100)
                .WithMessage("Bulk request cannot contain more than 100 records.");

            RuleForEach(x => x.Records)
                .NotNull()
                .WithMessage("Usage record cannot be null.");

            When(x => x.Records != null && x.Records.Any(), () =>
            {
                RuleForEach(x => x.Records)
                    .SetValidator(recordValidator);
            });
        }
    }
}
