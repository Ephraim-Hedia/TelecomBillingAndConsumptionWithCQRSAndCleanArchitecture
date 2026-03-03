using FluentValidation;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Features.UsageFeatures.Commands.Models;
using TelecomBillingAndConsumption.Core.Resources;
using TelecomBillingAndConsumption.Data.Helpers;

namespace TelecomBillingAndConsumption.Core.Features.UsageFeatures.Commands.Validators
{
    public class AddUsageRecordValidator : AbstractValidator<AddUsageRecordCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Constructors
        public AddUsageRecordValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _localizer = stringLocalizer;
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
        }
        #endregion
    }
}
