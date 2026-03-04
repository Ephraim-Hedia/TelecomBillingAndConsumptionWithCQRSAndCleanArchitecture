using FluentValidation;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Features.UsageFeatures.Commands.Models;
using TelecomBillingAndConsumption.Core.Resources;
using TelecomBillingAndConsumption.Data.Helpers;
using TelecomBillingAndConsumption.Service.Interfaces;

namespace TelecomBillingAndConsumption.Core.Features.UsageFeatures.Commands.Validators
{
    public class AddUsageRecordValidator : AbstractValidator<AddUsageRecordCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ISubscriberService _subscriberService;
        private readonly ITariffService _tariffService;
        #endregion
        #region Constructors
        public AddUsageRecordValidator(
            ISubscriberService subscriberService,
            ITariffService tariffService,
            IStringLocalizer<SharedResources> stringLocalizer)
        {

            _localizer = stringLocalizer;
            _tariffService = tariffService;
            _subscriberService = subscriberService;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Handle Functions

        public void ApplyValidationsRules()
        {
            RuleFor(x => x.SubscriberId)
                .GreaterThan(0).WithMessage("SubscriberId must be greater than 0.");
            RuleFor(x => x.Timestamp)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Timestamp cannot be in the future.");
            RuleFor(x => x.UsageType)
                .IsInEnum().WithMessage("Invalid UsageType value.");
            When(x => x.UsageType == UsageType.Data, () =>
            {
                RuleFor(x => x.DataMB)
                    .NotNull().WithMessage("DataMB is required for Data usage type.")
                    .GreaterThan(0).WithMessage("DataMB must be greater than 0.");
            });
            When(x => x.UsageType == UsageType.Call, () =>
            {
                RuleFor(x => x.CallMinutes)
                    .NotNull().WithMessage("CallMinutes is required for Call usage type.")
                    .GreaterThan(0).WithMessage("CallMinutes must be greater than 0.");
            });
            When(x => x.UsageType == UsageType.SMS, () =>
            {
                RuleFor(x => x.SMSCount)
                    .NotNull().WithMessage("SMSCount is required for SMS usage type.")
                    .GreaterThan(0).WithMessage("SMSCount must be greater than 0.");
            });
        }

        public void ApplyCustomValidationsRules()
        {

            RuleFor(x => x.SubscriberId)
            .GreaterThan(0).WithMessage(_localizer[SharedResourcesKeys.Required] + " Subscriber Id is required.")
            .MustAsync(async (subscriberId, cancellation) =>
            {
                var exists = await _subscriberService.GetByIdAsync(subscriberId);
                return exists != null;
            })
            .WithMessage(_localizer["Subscriber does not exist."]);

            // Tariff existence/business logic check
            RuleFor(x => x)
                .MustAsync(async (command, cancellation) =>
                {
                    // Get the subscriber (already validated)
                    var subscriber = await _subscriberService.GetByIdAsync(command.SubscriberId);

                    // Calculate IsPeak from Timestamp
                    var hour = command.Timestamp.Hour;
                    bool isPeak = hour >= 8 && hour < 20;
                    bool isRoaming = subscriber != null && subscriber.IsRoaming;

                    // Check for matching tariff
                    var tariff = await _tariffService.FindTariffAsync(
                        command.UsageType,
                        isRoaming,
                        isPeak
                    );
                    return tariff != null;
                })
                .WithMessage(_localizer["No valid tariff found for given UsageType, Roaming, and Peak conditions."]);
            #endregion
        }
    }
}
