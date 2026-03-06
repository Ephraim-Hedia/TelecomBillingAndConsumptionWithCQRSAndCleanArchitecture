using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelecomBillingAndConsumption.Api.Bases;
using TelecomBillingAndConsumption.Core.Features.ApplicationUser.Commands.Models;
using TelecomBillingAndConsumption.Core.Features.ApplicationUser.Queries.Models;
using TelecomBillingAndConsumption.Data.AppMetaData;

namespace TelecomBillingAndConsumption.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class ApplicationUserController : AppControllerBase
    {
        [HttpPost]
        [Route(Router.ApplicationUserRouting.create)]
        public async Task<IActionResult> CreateNewUser([FromBody] AddUserCommand command)
            => NewResult(await Mediator.Send(command));

        [HttpGet]
        [Route(Router.ApplicationUserRouting.getAllPaginated)]
        public async Task<IActionResult> GetAllUsersPaginatedList([FromQuery] GetUserPaginatedQuery query)
             => Ok(await Mediator.Send(query));

        [HttpGet]
        [Route(Router.ApplicationUserRouting.getById)]
        public async Task<IActionResult> GetUserById(int id)
             => Ok(await Mediator.Send(new GetUserByIdQuery(id)));

        [HttpPut]
        [Route(Router.ApplicationUserRouting.update)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
            => NewResult(await Mediator.Send(command));


        [HttpDelete]
        [Route(Router.ApplicationUserRouting.delete)]
        public async Task<IActionResult> DeleteUser(int id)
            => NewResult(await Mediator.Send(new DeleteUserCommand(id)));

        [HttpPut]
        [Route(Router.ApplicationUserRouting.changePassword)]
        public async Task<IActionResult> ChangeUserPassword([FromBody] ChangeUserPasswordCommand command)
            => NewResult(await Mediator.Send(command));
    }
}
