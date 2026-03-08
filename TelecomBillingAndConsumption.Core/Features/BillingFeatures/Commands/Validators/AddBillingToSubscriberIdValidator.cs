using TelecomBillingAndConsumption.Core.Features.BillingFeatures.Commands.Models;

namespace TelecomBillingAndConsumption.Core.Features.BillingFeatures.Commands.Validators
{
    using FluentValidation;
    using global::TelecomBillingAndConsumption.Service.Interfaces;
    using Microsoft.EntityFrameworkCore;

    namespace TelecomBillingAndConsumption.Core.Features.BillingFeatures.Commands.Validators
    {
        public class AddBillingToSubscriberIdValidator : AbstractValidator<AddBillingToSubscriberIdCommand>
        {
            private readonly ISubscriberService _subscriberService;
            private readonly IBillService _billService;

            public AddBillingToSubscriberIdValidator(
                ISubscriberService subscriberService,
                IBillService billService)
            {
                _subscriberService = subscriberService;
                _billService = billService;

                ApplyBasicValidation();
                ApplyBusinessValidation();
            }

            private void ApplyBasicValidation()
            {
                RuleFor(x => x.SubscriberId)
                    .GreaterThan(0)
                    .WithMessage("SubscriberId must be greater than 0.");

                RuleFor(x => x.Month)
                    .NotEmpty()
                    .WithMessage("Month is required.")
                    .Matches(@"^\d{4}-(0[1-9]|1[0-2])$")
                    .WithMessage("Month must be in format YYYY-MM.");
            }

            private void ApplyBusinessValidation()
            {
                RuleFor(x => x)
                    .MustAsync(async (command, cancellation) =>
                    {
                        var subscriber = await _subscriberService.GetByIdAsync(command.SubscriberId);
                        return subscriber != null;
                    })
                    .WithMessage("Subscriber does not exist.");

                RuleFor(x => x)
                    .MustAsync(async (command, cancellation) =>
                    {
                        var subscriber = await _subscriberService.GetByIdAsync(command.SubscriberId);
                        return subscriber != null && subscriber.IsActive;
                    })
                    .WithMessage("Cannot generate bill for inactive subscriber.");

                RuleFor(x => x)
                    .MustAsync(async (command, cancellation) =>
                    {
                        var exists = await _billService
                            .GetAllBillsBySubsriberIdQuarable(command.SubscriberId)
                            .AnyAsync(b => b.Month == command.Month);

                        return !exists;
                    })
                    .WithMessage("Bill already generated for this subscriber and month.");

                RuleFor(x => x.Month)
                    .Must(BePastOrCurrentMonth)
                    .WithMessage("Cannot generate bill for a future month.");
            }

            private bool BePastOrCurrentMonth(string month)
            {
                if (!DateTime.TryParse($"{month}-01", out var parsedDate))
                    return false;

                var now = DateTime.UtcNow;
                var currentMonth = new DateTime(now.Year, now.Month, 1);

                return parsedDate <= currentMonth;
            }
        }
    }
}
