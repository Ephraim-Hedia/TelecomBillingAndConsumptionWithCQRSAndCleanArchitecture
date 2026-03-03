using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.Authentication.Commands.Models;
using TelecomBillingAndConsumption.Core.Resources;
using TelecomBillingAndConsumption.Data.Entities.Identity;
using TelecomBillingAndConsumption.Service.Interfaces;

namespace TelecomBillingAndConsumption.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler : ResponseHandler
            , IRequestHandler<SignInCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthenticationService _authenticationService;
        #endregion
        #region Constructors
        public AuthenticationCommandHandler(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IAuthenticationService authenticationService,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _userManager = userManager;
            _localizer = stringLocalizer;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
        }
        #endregion
        #region Handle Functions
        public async Task<Response<string>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            // check if the user is valid
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == request.UserName);
            if (user == null)
            {
                return Unauthorized<string>(_localizer[SharedResourcesKeys.InvalidCredentials]);
            }
            // check if the password is valid
            //var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            //if (!isPasswordValid)
            //{
            //    return Unauthorized<string>(_localizer[SharedResourcesKeys.InvalidCredentials]);
            //}

            // another way to check if the password is valid
            var signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
            if (!signInResult.Succeeded)
            {
                return Unauthorized<string>(_localizer[SharedResourcesKeys.InvalidCredentials]);
            }


            // Generate JWT token
            var accessToken = await _authenticationService.GetJwtToken(user);
            return Success(accessToken);
        }
        #endregion


    }
}
