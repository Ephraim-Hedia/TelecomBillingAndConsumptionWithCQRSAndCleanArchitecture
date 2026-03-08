using FluentValidation;
using TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Models;
using TelecomBillingAndConsumption.Service.Interfaces;

namespace TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Validators
{
    public class GetAllBillingsBySubscriberIdValidator
        : AbstractValidator<GetAllBillingsBySubscriberIdQuery>
    {
        private readonly ISubscriberService _subscriberService;
        public GetAllBillingsBySubscriberIdValidator(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
            RuleFor(x => x.SubscriberId)
                .GreaterThan(0)
                .WithMessage("SubscriberId must be greater than 0.")
                .MustAsync(async (id, cancellation) =>
                {
                    var subscriber = await _subscriberService.GetByIdAsync(id);
                    return subscriber != null;
                })
                .WithMessage("Subscriber does not exist."); ;

            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageNumber must be at least 1.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100)
                .WithMessage("PageSize must be between 1 and 100.");

        }
    }
}
