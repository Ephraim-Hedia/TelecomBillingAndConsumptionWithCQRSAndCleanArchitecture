using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.ApplicationUser.Commands.Models;
using TelecomBillingAndConsumption.Core.Resources;
using TelecomBillingAndConsumption.Data.Entities.Identity;

namespace TelecomBillingAndConsumption.Core.Features.ApplicationUser.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler
            , IRequestHandler<AddUserCommand, Response<string>>
            , IRequestHandler<UpdateUserCommand, Response<string>>
            , IRequestHandler<DeleteUserCommand, Response<string>>
            , IRequestHandler<ChangeUserPasswordCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        #endregion


        #region Constructors
        public UserCommandHandler(
            IMapper mapper,
            UserManager<User> userManager,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _mapper = mapper;
            _userManager = userManager;
            _localizer = stringLocalizer;
        }
        #endregion



        #region Handle Functions
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            // Check if the email is exist 
            var isEmailExist = await _userManager.FindByEmailAsync(request.Email);

            if (isEmailExist != null)
                return BadRequest<string>(_localizer[SharedResourcesKeys.EmailIsExist]);

            // Check if the UserName is Exist or not 
            var isUserNameExist = await _userManager.FindByNameAsync(request.UserName);
            if (isUserNameExist != null)
                return BadRequest<string>(_localizer[SharedResourcesKeys.UserNameIsExist]);


            // Create New User
            var identityUser = _mapper.Map<User>(request);


            var createResult = await _userManager.CreateAsync(identityUser, request.Password);

            if (!createResult.Succeeded)
                return BadRequest<string>(createResult.Errors.FirstOrDefault().Description);

            await _userManager.AddToRoleAsync(identityUser, "User");
            return Created("");
        }

        public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            // check if the user is exist or not 
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) return NotFound<string>();

            // mapping 
            var newUser = _mapper.Map(request, user);

            var response = await _userManager.UpdateAsync(newUser);
            if (!response.Succeeded)
                return BadRequest<string>(_localizer[SharedResourcesKeys.UpdateFailed]);

            return Updated<string>();
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            // check if the user is exist or not
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) return NotFound<string>();


            var response = await _userManager.DeleteAsync(user);
            if (!response.Succeeded)
                return BadRequest<string>(_localizer[SharedResourcesKeys.DeletedFailed]);


            return Deleted<string>();

        }

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) return NotFound<string>();

            var response = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!response.Succeeded)
                return BadRequest<string>(response.Errors.FirstOrDefault().Description);
            return Updated<string>();

        }
        #endregion
    }
}
