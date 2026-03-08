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
        private readonly ISubscriberService _subscriberService;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public AddUsageRecordValidator(
            ISubscriberService subscriberService,
            IStringLocalizer<SharedResources> stringLocalizer)
        {
            _subscriberService = subscriberService;
            _localizer = stringLocalizer;

            ApplyBasicValidation();
            ApplyUsageTypeValidation();
            ApplyCustomValidation();
        }

        private void ApplyBasicValidation()
        {
            RuleFor(x => x.SubscriberId)
                .GreaterThan(0)
                .WithMessage("SubscriberId must be greater than 0.");

            RuleFor(x => x.Timestamp)
                .NotEmpty()
                .WithMessage("Timestamp is required.")
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Usage timestamp cannot be in the future.");

            RuleFor(x => x.UsageType)
                .IsInEnum()
                .WithMessage("Invalid UsageType value.");
        }

        private void ApplyUsageTypeValidation()
        {
            When(x => x.UsageType == UsageType.Call, () =>
            {
                RuleFor(x => x.CallMinutes)
                    .NotNull()
                    .WithMessage("CallMinutes is required for Call usage.")
                    .GreaterThan(0)
                    .WithMessage("CallMinutes must be greater than 0.")
                    .LessThanOrEqualTo(120)
                    .WithMessage("CallMinutes cannot exceed 120 minutes per record.");

                RuleFor(x => x.DataMB)
                    .Null()
                    .WithMessage("DataMB must be null when UsageType is Call.");

                RuleFor(x => x.SMSCount)
                    .Null()
                    .WithMessage("SMSCount must be null when UsageType is Call.");
            });

            When(x => x.UsageType == UsageType.Data, () =>
            {
                RuleFor(x => x.DataMB)
                    .NotNull()
                    .WithMessage("DataMB is required for Data usage.")
                    .GreaterThan(0)
                    .WithMessage("DataMB must be greater than 0.")
                    .LessThanOrEqualTo(5000)
                    .WithMessage("DataMB cannot exceed 5000 MB per record.");

                RuleFor(x => x.CallMinutes)
                    .Null()
                    .WithMessage("CallMinutes must be null when UsageType is Data.");

                RuleFor(x => x.SMSCount)
                    .Null()
                    .WithMessage("SMSCount must be null when UsageType is Data.");
            });

            When(x => x.UsageType == UsageType.SMS, () =>
            {
                RuleFor(x => x.SMSCount)
                    .NotNull()
                    .WithMessage("SMSCount is required for SMS usage.")
                    .GreaterThan(0)
                    .WithMessage("SMSCount must be greater than 0.")
                    .LessThanOrEqualTo(50)
                    .WithMessage("SMSCount cannot exceed 50 messages per record.");

                RuleFor(x => x.CallMinutes)
                    .Null()
                    .WithMessage("CallMinutes must be null when UsageType is SMS.");

                RuleFor(x => x.DataMB)
                    .Null()
                    .WithMessage("DataMB must be null when UsageType is SMS.");
            });
        }

        private void ApplyCustomValidation()
        {
            RuleFor(x => x.SubscriberId)
                .MustAsync(async (subscriberId, cancellation) =>
                {
                    var subscriber = await _subscriberService.GetByIdAsync(subscriberId);
                    return subscriber != null;
                })
                .WithMessage("Subscriber does not exist.");
        }
    }
}
