using FluentValidation;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Commands.Models;
using TelecomBillingAndConsumption.Core.Resources;
using TelecomBillingAndConsumption.Service.Interfaces;

namespace TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Commands.Validators
{
    public class ActivateSubscriberByIdValidator : AbstractValidator<ActivateSubscriberByIdCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ISubscriberService _subscriberService;
        #endregion
        #region Constructors
        public ActivateSubscriberByIdValidator(
            ISubscriberService subscriberService,
            IStringLocalizer<SharedResources> stringLocalizer)
        {
            _subscriberService = subscriberService;
            _localizer = stringLocalizer;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Handle Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id)
                            .MustAsync(async (id, cancellation) =>
                            {
                                var subscriber = await _subscriberService.GetByIdAsync(id);
                                return subscriber != null;
                            })
                            .WithMessage("Subscriber does not exist.");
        }

        public void ApplyCustomValidationsRules()
        {
        }

        #endregion
    }
}
