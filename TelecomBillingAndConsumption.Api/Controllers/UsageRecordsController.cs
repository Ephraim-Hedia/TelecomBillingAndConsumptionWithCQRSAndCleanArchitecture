
using Microsoft.AspNetCore.Mvc;
using TelecomBillingAndConsumption.Api.Bases;
using TelecomBillingAndConsumption.Core.Features.UsageFeatures.Commands.Models;
using TelecomBillingAndConsumption.Core.Features.UsageFeatures.Queries.Models;
using TelecomBillingAndConsumption.Data.AppMetaData;

namespace TelecomBillingAndConsumption.Api.Controllers
{
    [ApiController]
    public class UsageRecordsController : AppControllerBase
    {

        //[HttpGet]
        //[Route(Router.UsageRecords.getAllPaginated)]
        //public async Task<IActionResult> GetAllPaginated([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        //{
        //    return Ok(await Mediator.Send(new GetAllUsageRecordsPaginatedQuery { PageNumber = pageNumber, PageSize = pageSize }));
        //}

        [HttpGet(Router.UsageRecords.getById)]
        public async Task<IActionResult> GetUsageRecordById(int id)
        {
            return Ok(await Mediator.Send(new GetUsageRecordByIdQuery() { Id = id }));
        }

        [HttpGet]
        [Route(Data.AppMetaData.Router.UsageRecords.getBySubscriberId)]
        public async Task<IActionResult> GetBySubscriberId([FromRoute] int subscriberId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return Ok(await Mediator.Send(new GetUsageRecordsBySubscriberIdQuery { SubscriberId = subscriberId, PageNumber = pageNumber, PageSize = pageSize }));
        }

        [HttpPost(Router.UsageRecords.create)]
        public async Task<IActionResult> CreateUsageRecord([FromBody] AddUsageRecordCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete(Router.UsageRecords.delete)]
        public async Task<IActionResult> DeleteUsageRecord(int id)
        {
            return Ok(await Mediator.Send(new DeleteUsageRecordByIdCommand() { Id = id }));
        }
    }
}
