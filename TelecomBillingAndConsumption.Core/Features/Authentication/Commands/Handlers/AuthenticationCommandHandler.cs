using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.Authentication.Commands.Models;
using TelecomBillingAndConsumption.Core.Resources;
using TelecomBillingAndConsumption.Data.Entities.Identity;
using TelecomBillingAndConsumption.Data.Results;
using TelecomBillingAndConsumption.Service.Interfaces;

namespace TelecomBillingAndConsumption.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler : ResponseHandler
            , IRequestHandler<SignInCommand, Response<JwtAuthResult>>
            , IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>

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
        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            // check if the user is valid
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == request.UserName);
            if (user == null)
            {
                return Unauthorized<JwtAuthResult>(_localizer[SharedResourcesKeys.InvalidCredentials]);
            }
            // check if the password is valid
            //var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            //if (!isPasswordValid)
            //{
            //    return Unauthorized<JwtAuthResult>(_localizer[SharedResourcesKeys.InvalidCredentials]);
            //}

            // another way to check if the password is valid
            var signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
            if (!signInResult.Succeeded)
            {
                return Unauthorized<JwtAuthResult>(_localizer[SharedResourcesKeys.InvalidCredentials]);
            }


            // Generate JWT token
            var accessToken = await _authenticationService.GetJwtToken(user);
            return Success(accessToken);
        }

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            // read Token to get claims
            // validation token , refresh token 
            // get user 
            // token is expired or not => generate refersh token

            var jwtToken = _authenticationService.ReadJWTToken(request.AccessToken);
            var userIdAndExpireDate = await _authenticationService.ValidateDetails(jwtToken, request.AccessToken, request.RefreshToken);
            switch (userIdAndExpireDate)
            {
                case ("AlgorithmIsWrong", null): return Unauthorized<JwtAuthResult>(_localizer[SharedResourcesKeys.AlgorithmIsWrong]);
                case ("TokenIsNotExpired", null): return Unauthorized<JwtAuthResult>(_localizer[SharedResourcesKeys.TokenIsNotExpired]);
                case ("RefreshTokenIsNotFound", null): return Unauthorized<JwtAuthResult>(_localizer[SharedResourcesKeys.RefreshTokenIsNotFound]);
                case ("RefreshTokenIsExpired", null): return Unauthorized<JwtAuthResult>(_localizer[SharedResourcesKeys.RefreshTokenIsExpired]);
            }
            var (userId, expiryDate) = userIdAndExpireDate;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound<JwtAuthResult>();
            }
            var result = await _authenticationService.GetRefreshToken(user, jwtToken, expiryDate, request.RefreshToken);
            return Success(result);
        }
        #endregion


    }
}
