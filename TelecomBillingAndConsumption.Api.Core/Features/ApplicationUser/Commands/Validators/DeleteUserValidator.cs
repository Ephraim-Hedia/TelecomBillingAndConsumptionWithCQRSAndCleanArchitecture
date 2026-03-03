using FluentValidation;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Features.ApplicationUser.Commands.Models;
using TelecomBillingAndConsumption.Core.Resources;

namespace TelecomBillingAndConsumption.Core.Features.ApplicationUser.Commands.Validators
{
    public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
    {

        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Constructors
        public DeleteUserValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _localizer = stringLocalizer;
            ValidateDeleteUserCommand();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Handle Functions
        void ValidateDeleteUserCommand()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
        void ApplyCustomValidationsRules()
        {
        }
        #endregion
    }
}
