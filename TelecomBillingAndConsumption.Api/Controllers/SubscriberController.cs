using Microsoft.AspNetCore.Mvc;
using TelecomBillingAndConsumption.Api.Bases;
using TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Commands.Models;
using TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Queries.Models;
using TelecomBillingAndConsumption.Data.AppMetaData;

namespace TelecomBillingAndConsumption.Api.Controllers
{
    [ApiController]
    public class SubscriberController : AppControllerBase
    {


        [HttpGet(Router.Subscribers.getAllPaginated)]
        public async Task<IActionResult> GetAllPaginated([FromQuery] GetAllSubscribersPaginatedQuery query)
        => Ok(await Mediator.Send(query));

        [HttpGet(Router.Subscribers.getById)]
        public async Task<IActionResult> GetById(int id)
        => Ok(await Mediator.Send(new GetSubscriberByIdQuery(id)));

        [HttpPost(Router.Subscribers.create)]
        public async Task<IActionResult> Create([FromBody] AddSubscriberCommand command)
        => Ok(await Mediator.Send(command));

        [HttpPut]
        [Route(Router.Subscribers.update)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSubscriberByIdCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete(Router.Subscribers.delete)]
        public async Task<IActionResult> Delete(int id)
        => NewResult(await Mediator.Send(new DeleteSubscriberByIdCommand(id)));

        [HttpPut(Router.Subscribers.activate)]
        public async Task<IActionResult> Activate(int id)
        => NewResult(await Mediator.Send(new ActivateSubscriberByIdCommand(id)));

        [HttpPut(Router.Subscribers.deactivate)]
        public async Task<IActionResult> Deactivate(int id)
            => NewResult(await Mediator.Send(new DeactivateSubscriberByIdCommand(id)));

        [HttpGet(Router.Subscribers.SubscriberUsageSummary)]
        public async Task<IActionResult> GetByIdSubscriberUsageSummary(int id)
        => Ok(await Mediator.Send(new GetSubscriberUsageSummaryQuery(id)));
    }
}
