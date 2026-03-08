
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelecomBillingAndConsumption.Api.Bases;
using TelecomBillingAndConsumption.Api.Extensions;
using TelecomBillingAndConsumption.Core.Features.UsageFeatures.Commands.Models;
using TelecomBillingAndConsumption.Core.Features.UsageFeatures.Queries.Models;
using TelecomBillingAndConsumption.Data.AppMetaData;

namespace TelecomBillingAndConsumption.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class UsageRecordsController : AppControllerBase
    {

        // ==============================
        // USER ENDPOINT
        // ==============================

        [HttpGet("me/usage")]
        public async Task<IActionResult> GetMyUsage(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var subscriberId = User.GetSubscriberId();

            if (subscriberId == null)
                return Unauthorized();

            var result = await Mediator.Send(new GetUsageRecordsBySubscriberIdQuery
            {
                SubscriberId = subscriberId.Value,
                PageNumber = pageNumber,
                PageSize = pageSize
            });


            return Ok(result);
        }


        // ==============================
        // ADMIN ENDPOINTS
        // ==============================

        //[Authorize(Roles = "Admin")]
        //[HttpGet]
        //[Route(Router.UsageRecords.getAllPaginated)]
        //public async Task<IActionResult> GetAllPaginated([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        //{
        //    return Ok(await Mediator.Send(new GetAllUsageRecordsPaginatedQuery { PageNumber = pageNumber, PageSize = pageSize }));
        //}

        [Authorize(Roles = "Admin")]
        [HttpGet(Router.UsageRecords.getById)]
        public async Task<IActionResult> GetUsageRecordById(int id, [FromQuery] string responseFormat = "json")
        {
            var result = await Mediator.Send(new GetUsageRecordByIdQuery() { Id = id });

            if (responseFormat.ToLower() == "soap" || responseFormat.ToLower() == "xml")
            {
                var xml = SerializeToSoap.Serialize(result);
                return Content(xml, "application/soap+xml");
            }

            return Ok(result);
        }



        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route(Router.UsageRecords.getBySubscriberId)]
        public async Task<IActionResult> GetBySubscriberId([FromRoute] int subscriberId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return Ok(await Mediator.Send(new GetUsageRecordsBySubscriberIdQuery { SubscriberId = subscriberId, PageNumber = pageNumber, PageSize = pageSize }));
        }


        [Authorize(Roles = "Admin")]
        [HttpPost(Router.UsageRecords.create)]
        public async Task<IActionResult> CreateUsageRecord([FromBody] AddUsageRecordCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete(Router.UsageRecords.delete)]
        public async Task<IActionResult> DeleteUsageRecord(int id)
        {
            return Ok(await Mediator.Send(new DeleteUsageRecordByIdCommand() { Id = id }));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost(Router.UsageRecords.bulk)]
        public async Task<IActionResult> AddUsageBulk([FromBody] AddUsageRecordsBulkCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
