using FluentValidation;
using TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Models;
using TelecomBillingAndConsumption.Service.Interfaces;

namespace TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Validators
{
    public class GetBillBySubscriberIdAndMonthValidator
        : AbstractValidator<GetBillBySubscriberIdAndMonthQuery>
    {
        private readonly ISubscriberService _subscriberService;
        public GetBillBySubscriberIdAndMonthValidator(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
            RuleFor(x => x.SubscriberId)
                .GreaterThan(0)
                .WithMessage("SubscriberId must be greater than 0.")
                .MustAsync(async (id, cancellation) =>
                {
                    var subscriber = await _subscriberService.GetByIdAsync(id);
                    return subscriber != null;
                }).WithMessage("Subscriber does not exist.");

            RuleFor(x => x.Month)
                .NotEmpty()
                .WithMessage("Month is required.")
                .Matches(@"^\d{4}-(0[1-9]|1[0-2])$")
                .WithMessage("Month must be in format YYYY-MM.");

        }
    }
}
