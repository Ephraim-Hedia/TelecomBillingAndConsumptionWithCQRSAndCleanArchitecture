using Microsoft.AspNetCore.Mvc;
using TelecomBillingAndConsumption.Api.Bases;
using TelecomBillingAndConsumption.Core.Features.TariffsFeatures.Commands.Models;
using TelecomBillingAndConsumption.Core.Features.TariffsFeatures.Queries.Models;
using TelecomBillingAndConsumption.Data.AppMetaData;

namespace TelecomBillingAndConsumption.Api.Controllers
{
    [ApiController]
    public class TariffController : AppControllerBase
    {
        // AddTariffRuleCommand
        // DeleteTariffRuleByIdCommand
        // UpdateTariffRuleByIdCommand
        // GetAllTariffsRulesQuery
        // GetTariffRuleByIdQuery

        //| UsageType | IsRoaming | IsPeak | PricePerUnit |
        //| --------- | --------- | ------ | ------------ |
        //| Call      | false     | true   | 0.15         |
        //| Call      | false     | false  | 0.05         |
        //| Data      | false     | false  | 0.05         |
        //| Data      | true      | false  | 0.20         |
        //| SMS       | false     | false  | 0.02         |
        //| SMS       | true      | false  | 0.10         |


        [HttpPost(Router.TariffRouting.create)]
        public async Task<IActionResult> Create([FromBody] AddTariffRuleCommand command)
            => NewResult(await Mediator.Send(command));

        [HttpDelete(Router.TariffRouting.delete)]
        public async Task<IActionResult> Delete(int id)
            => NewResult(await Mediator.Send(new DeleteTariffRuleByIdCommand(id)));

        [HttpPut(Router.TariffRouting.update)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTariffRuleByIdCommand command)
        {
            command.Id = id;
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpGet(Router.TariffRouting.getAll)]
        public async Task<IActionResult> GetAll()
            => Ok(await Mediator.Send(new GetAllTariffRulesQuery()));

        [HttpGet(Router.TariffRouting.getById)]
        public async Task<IActionResult> GetById(int id)
            => Ok(await Mediator.Send(new GetTariffRuleByIdQuery(id)));
    }
}
