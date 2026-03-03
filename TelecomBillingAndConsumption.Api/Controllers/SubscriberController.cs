using Microsoft.AspNetCore.Mvc;
using TelecomBillingAndConsumption.Api.Bases;
using TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Commands.Models;
using TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Queries.Models;

namespace TelecomBillingAndConsumption.Api.Controllers
{
    [ApiController]
    public class SubscriberController : AppControllerBase
    {
        [HttpGet]
        [Route(Data.AppMetaData.Router.Subscribers.getAllPaginated)]
        public async Task<IActionResult> GetAllPaginated([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return Ok(await Mediator.Send(new GetAllSubscribersPaginatedQuery() { PageNumber = pageNumber, PageSize = pageSize }));
        }

        [HttpGet]
        [Route(Data.AppMetaData.Router.Subscribers.getById)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await Mediator.Send(new GetSubscriberByIdQuery() { Id = id }));
        }
        [HttpPost]
        [Route(Data.AppMetaData.Router.Subscribers.create)]
        public async Task<IActionResult> Create([FromBody] AddSubscriberCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        [Route(Data.AppMetaData.Router.Subscribers.update)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSubscriberByIdCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete]
        [Route(Data.AppMetaData.Router.Subscribers.delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return Ok(await Mediator.Send(new DeleteSubscriberByIdCommand(id)));
        }
    }
}
