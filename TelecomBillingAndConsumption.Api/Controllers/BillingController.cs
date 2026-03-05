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
        [HttpPost]
        [Route(Router.BillingRouting.create)]
        public async Task<IActionResult> Create([FromBody] AddBillingToSubscriberIdCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }
        [HttpGet(Router.BillingRouting.getBySubscriberId)]
        public async Task<IActionResult> GetAllPaginated(int subscriberId)
        {
            var result = await Mediator.Send(new GetAllBillingsBySubscriberIdQuery() { SubscriberId = subscriberId });
            return Ok(result);
        }

        [HttpGet(Router.BillingRouting.getBySubscriberIdAndMonth)]
        public async Task<IActionResult> GetBySubscriberIdAndMonth(int subscriberId, string month)
        {
            var result = await Mediator.Send(new GetBillBySubscriberIdAndMonthQuery() { SubscriberId = subscriberId, Month = month });
            return Ok(result);
        }

        // GetBillByIdQuery
        [HttpGet]
        [Route(Router.BillingRouting.getById)]
        public async Task<IActionResult> GetById([FromRoute] int id, [FromQuery] string responseFormat = "json")
        {
            var result = await Mediator.Send(new GetBillByIdQuery() { BillId = id });
            if (responseFormat.ToLower() == "soap" || responseFormat.ToLower() == "xml")
            {
                var xml = SerializeToSoap.Serialize(result); // Or standard xml
                return Content(xml, "application/soap+xml");
            }
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


    }
}
