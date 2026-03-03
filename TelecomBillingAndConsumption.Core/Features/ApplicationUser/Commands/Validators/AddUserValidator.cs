using FluentValidation;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Features.ApplicationUser.Commands.Models;
using TelecomBillingAndConsumption.Core.Resources;

namespace TelecomBillingAndConsumption.Core.Features.ApplicationUser.Commands.Validators
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Constructors
        public AddUserValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _localizer = stringLocalizer;
            ValidateAddUserCommand();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Handle Functions
        void ValidateAddUserCommand()
        {
            RuleFor(x => x.UserName)
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(50).WithMessage("UserName Max Length is 50 Char.");
            RuleFor(x => x.FullName)
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(50).WithMessage("FullName Max Length is 50 Char.");
            RuleFor(x => x.Email)
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email Max Length is 100 Char.");
            RuleFor(x => x.Password)
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .MaximumLength(100).WithMessage("Password Max Length is 100 Char.");

            RuleFor(x => x.ConfirmPassword)
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .Equal(x => x.Password).WithMessage(_localizer[SharedResourcesKeys.PasswordNotEqualConfirmPass]);

            RuleFor(x => x.Address)
            .MaximumLength(200).WithMessage("Address Max Length is 200 Char.");
            RuleFor(x => x.Country)
            .MaximumLength(50).WithMessage("Country Max Length is 50 Char.");
            RuleFor(x => x.PhoneNumber)
            .MaximumLength(11).WithMessage("PhoneNumber Max Length is 11 Char.");


        }
        void ApplyCustomValidationsRules()
        {

        }
        #endregion

    }
}
