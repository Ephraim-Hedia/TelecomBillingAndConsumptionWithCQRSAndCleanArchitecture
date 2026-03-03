using Microsoft.AspNetCore.Mvc;
using TelecomBillingAndConsumption.Api.Bases;
using TelecomBillingAndConsumption.Core.Features.Authentication.Commands.Models;
using TelecomBillingAndConsumption.Data.AppMetaData;

namespace TelecomBillingAndConsumption.Api.Controllers
{
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        [HttpPost]
        [Route(Router.AuthenticationRouting.signIn)]
        public async Task<IActionResult> CreateStudent([FromBody] SignInCommand command)
            => NewResult(await Mediator.Send(command));
    }
}
