using Microsoft.AspNetCore.Mvc;
using TelecomBillingAndConsumption.Api.Bases;
using TelecomBillingAndConsumption.Core.Features.PlansFeatures.Commands.Models;
using TelecomBillingAndConsumption.Core.Features.PlansFeatures.Queries.Models;
using TelecomBillingAndConsumption.Data.AppMetaData;

namespace TelecomBillingAndConsumption.Api.Controllers
{
    [ApiController]
    public class PlansController : AppControllerBase
    {
        [HttpGet(Router.PlansRouting.getAllPaginated)]
        public async Task<IActionResult> GetAllPaginated()
        {
            var result = await Mediator.Send(new GetAllPlansPaginatedQuery());
            return Ok(result);
        }


        [HttpGet(Router.PlansRouting.getById)]
        public async Task<IActionResult> GetById(int id, [FromQuery] string responseFormat = "json")
        {
            var result = await Mediator.Send(new GetPlanByIdQuery(id));
            if (responseFormat.ToLower() == "soap" || responseFormat.ToLower() == "xml")
            {
                var xml = SerializeToSoap.Serialize(result); // Or standard xml
                return Content(xml, "application/soap+xml");
            }
            return Ok(result);
        }



        [HttpPost(Router.PlansRouting.create)]
        public async Task<IActionResult> Create([FromBody] AddPlanCommand command)
             => NewResult(await Mediator.Send(command));

        [HttpPut(Router.PlansRouting.update)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePlanByIdCommand command)
        {
            command.Id = id;
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete(Router.PlansRouting.delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await Mediator.Send(new DeletePlanByIdCommand(id));
            return NewResult(response);
        }

        [HttpPut(Router.PlansRouting.activate)]
        public async Task<IActionResult> Activate([FromRoute] int id)
        {
            var response = await Mediator.Send(new ActivatePlanByIdCommand(id));
            return NewResult(response);
        }

        [HttpPut(Router.PlansRouting.deactivate)]
        public async Task<IActionResult> Deactivate([FromRoute] int id)
        {
            var response = await Mediator.Send(new DeactivatePlanByIdCommand(id));
            return NewResult(response);
        }
    }
}
