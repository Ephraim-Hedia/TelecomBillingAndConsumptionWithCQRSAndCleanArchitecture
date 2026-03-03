using FluentValidation;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Features.ApplicationUser.Commands.Models;
using TelecomBillingAndConsumption.Core.Resources;

namespace TelecomBillingAndConsumption.Core.Features.ApplicationUser.Commands.Validators
{
    public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Constructors
        public ChangeUserPasswordValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _localizer = stringLocalizer;
            ValidateChangeUserPasswordCommand();
            ApplyCustomValidationsRules();
        }
        #endregion
        public void ValidateChangeUserPasswordCommand()
        {
            RuleFor(x => x.UserId)
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .GreaterThan(0).WithMessage("UserId must be greater than 0.");
            RuleFor(x => x.CurrentPassword)
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .MinimumLength(6).WithMessage("Current Password must be at least 6 characters long.")
                .MaximumLength(100).WithMessage("Current Password Max Length is 100 Char.");
            RuleFor(x => x.NewPassword)
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .MinimumLength(6).WithMessage("New Password must be at least 6 characters long.")
                .MaximumLength(100).WithMessage("New Password Max Length is 100 Char.");
            RuleFor(x => x.ConfirmNewPassword)
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .Equal(x => x.NewPassword).WithMessage(_localizer[SharedResourcesKeys.PasswordNotEqualConfirmPass]);
        }
        public void ApplyCustomValidationsRules()
        {

        }
    }
}
