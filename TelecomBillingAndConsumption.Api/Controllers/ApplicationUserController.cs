using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelecomBillingAndConsumption.Api.Bases;
using TelecomBillingAndConsumption.Api.Extensions;
using TelecomBillingAndConsumption.Core.Features.ApplicationUser.Commands.Models;
using TelecomBillingAndConsumption.Core.Features.ApplicationUser.Queries.Models;
using TelecomBillingAndConsumption.Data.AppMetaData;

namespace TelecomBillingAndConsumption.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class ApplicationUserController : AppControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route(Router.ApplicationUserRouting.create)]
        public async Task<IActionResult> CreateNewUser([FromBody] AddUserCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route(Router.ApplicationUserRouting.getAllPaginated)]
        public async Task<IActionResult> GetAllUsersPaginatedList([FromQuery] GetUserPaginatedQuery query)
             => Ok(await Mediator.Send(query));

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route(Router.ApplicationUserRouting.getById)]
        public async Task<IActionResult> GetUserById(int id)
             => Ok(await Mediator.Send(new GetUserByIdQuery(id)));

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route(Router.ApplicationUserRouting.update)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
            => NewResult(await Mediator.Send(command));

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route(Router.ApplicationUserRouting.delete)]
        public async Task<IActionResult> DeleteUser(int id)
            => NewResult(await Mediator.Send(new DeleteUserCommand(id)));

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route(Router.ApplicationUserRouting.changeUserPassword)]
        public async Task<IActionResult> ChangeUserPassword([FromBody] ChangeUserPasswordCommand command)
            => NewResult(await Mediator.Send(command));



        [HttpPut(Router.ApplicationUserRouting.changeMyPassword)]
        public async Task<IActionResult> ChangeMyPassword([FromBody] ChangeMyPasswordCommand command)
        {
            var userId = User.GetUserId();

            if (userId == null)
                return Unauthorized();

            command.UserId = userId;

            var result = await Mediator.Send(command);

            return NewResult(result);
        }
    }
}
