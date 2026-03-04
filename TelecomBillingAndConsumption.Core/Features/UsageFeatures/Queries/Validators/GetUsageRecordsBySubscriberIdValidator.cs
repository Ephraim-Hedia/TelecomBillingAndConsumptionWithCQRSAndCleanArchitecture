using FluentValidation;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Features.UsageFeatures.Queries.Models;
using TelecomBillingAndConsumption.Core.Resources;
using TelecomBillingAndConsumption.Service.Interfaces;

namespace TelecomBillingAndConsumption.Core.Features.UsageFeatures.Queries.Validators
{
    public class GetUsageRecordsBySubscriberIdValidator : AbstractValidator<GetUsageRecordsBySubscriberIdQuery>
    {
        #region Fields
        private readonly ISubscriberService _subscriberService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public GetUsageRecordsBySubscriberIdValidator(
            ISubscriberService subscriberService,
            IStringLocalizer<SharedResources> localizer)
        {
            _subscriberService = subscriberService;
            _localizer = localizer;

            RuleFor(x => x.SubscriberId)
                .GreaterThan(0).WithMessage(_localizer[SharedResourcesKeys.Required] + " Subscriber Id is required.")
                .MustAsync(async (subscriberId, cancellation) =>
                {
                    var exists = await _subscriberService.GetByIdAsync(subscriberId);
                    return exists != null;
                })
                .WithMessage(_localizer["Subscriber does not exist."]);
        }
        #endregion
    }
}
