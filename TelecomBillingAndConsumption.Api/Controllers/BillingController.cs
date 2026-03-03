using Microsoft.AspNetCore.Mvc;
using TelecomBillingAndConsumption.Api.Bases;
using TelecomBillingAndConsumption.Core.Features.BillingFeatures.Commands.Models;
using TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Models;
using TelecomBillingAndConsumption.Data.AppMetaData;

namespace TelecomBillingAndConsumption.Api.Controllers
{
    [ApiController]
    public class BillingController : AppControllerBase
    {
        [HttpGet(Router.BillingRouting.getAllPaginated)]
        public async Task<IActionResult> GetAllPaginated([FromQuery] GetAllBillingsBySubscriberIdQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Route(Router.BillingRouting.create)]
        public async Task<IActionResult> Create([FromBody] AddBillingToSubscriberIdCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }
        // GetBillByIdQuery
        [HttpGet]
        [Route(Router.BillingRouting.getById)]
        public async Task<IActionResult> GetById([FromRoute] GetBillByIdQuery query)
        {
            var result = await Mediator.Send(query);
            return NewResult(result);
        }
        // GetBillingDetailsByBillIdQuery
        [HttpGet]
        [Route(Router.BillingRouting.getBillingDetailsByBillId)]
        public async Task<IActionResult> GetBillingDetailsByBillId([FromRoute] GetBillingDetailsByBillIdQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }


        // GetBillingHistoryForSubscriberQuery
        [HttpGet]
        [Route(Router.BillingRouting.getBillingHistoryForSubscriber)]
        public async Task<IActionResult> GetBillingHistoryForSubscriber([FromRoute] GetBillingHistoryForSubscriberQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
