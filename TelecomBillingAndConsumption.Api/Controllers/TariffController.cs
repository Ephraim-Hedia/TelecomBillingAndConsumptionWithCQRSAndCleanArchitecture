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

        [HttpPost]
        [Route(Router.TariffRouting.create)]
        public async Task<IActionResult> AddTariffRule([FromBody] AddTariffRuleCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpDelete]
        [Route(Router.TariffRouting.delete)]
        public async Task<IActionResult> DeleteTariffRuleById([FromRoute] DeleteTariffRuleByIdCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPut]
        [Route(Router.TariffRouting.update)]
        public async Task<IActionResult> UpdateTariffRuleById([FromRoute] UpdateTariffRuleByIdCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpGet]
        [Route(Router.TariffRouting.getAll)]
        public async Task<IActionResult> GetAllTariffsRules()
        {
            var response = await Mediator.Send(new GetAllTariffsRulesQuery());
            return Ok(response);
        }

        [HttpGet]
        [Route(Router.TariffRouting.getById)]
        public async Task<IActionResult> GetTariffRuleById([FromRoute] GetTariffRuleByIdQuery query)
        {
            var response = await Mediator.Send(query);
            return NewResult(response);
        }
    }
}
