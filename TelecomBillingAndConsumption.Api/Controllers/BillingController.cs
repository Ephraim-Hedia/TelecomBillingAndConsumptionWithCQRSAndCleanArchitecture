using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelecomBillingAndConsumption.Api.Bases;
using TelecomBillingAndConsumption.Api.Extensions;
using TelecomBillingAndConsumption.Core.Features.BillingFeatures.Commands.Models;
using TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Models;
using TelecomBillingAndConsumption.Data.AppMetaData;

namespace TelecomBillingAndConsumption.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class BillingController : AppControllerBase
    {


        // ==============================
        // USER ENDPOINTS
        // ==============================

        [HttpGet(Router.BillingRouting.GetMyBills)]
        public async Task<IActionResult> GetMyBills()
        {
            var subscriberId = User.GetSubscriberId();

            if (subscriberId == null)
                return Unauthorized();

            var result = await Mediator.Send(new GetAllBillingsBySubscriberIdQuery
            {
                SubscriberId = subscriberId.Value
            });

            return Ok(result);
        }

        [HttpGet(Router.BillingRouting.GetMyBillsByMonth)]
        public async Task<IActionResult> GetMyBillsByMonth(
            [FromQuery] string month,
            [FromQuery] string responseFormat = "json")
        {
            var subscriberId = User.GetSubscriberId();

            if (subscriberId == null)
                return Unauthorized();

            var result = await Mediator.Send(new GetBillBySubscriberIdAndMonthQuery
            {
                SubscriberId = subscriberId.Value,
                Month = month
            });

            if (responseFormat.ToLower() == "soap" || responseFormat.ToLower() == "xml")
            {
                var xml = SerializeToSoap.Serialize(result);
                return Content(xml, "application/soap+xml");
            }

            return Ok(result);
        }

        // ==============================
        // ADMIN ENDPOINTS
        // ==============================

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route(Router.BillingRouting.create)]
        public async Task<IActionResult> Create([FromBody] AddBillingToSubscriberIdCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet(Router.BillingRouting.getBySubscriberId)]
        public async Task<IActionResult> GetAllPaginated(int subscriberId)
        {
            var result = await Mediator.Send(new GetAllBillingsBySubscriberIdQuery() { SubscriberId = subscriberId });
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet(Router.BillingRouting.getBySubscriberIdAndMonth)]
        public async Task<IActionResult> GetBySubscriberIdAndMonth(int subscriberId, string month, [FromQuery] string responseFormat = "json")
        {
            var result = await Mediator.Send(new GetBillBySubscriberIdAndMonthQuery() { SubscriberId = subscriberId, Month = month });
            if (responseFormat.ToLower() == "soap" || responseFormat.ToLower() == "xml")
            {
                var xml = SerializeToSoap.Serialize(result); // Or standard xml
                return Content(xml, "application/soap+xml");
            }
            return NewResult(result);
        }

        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
