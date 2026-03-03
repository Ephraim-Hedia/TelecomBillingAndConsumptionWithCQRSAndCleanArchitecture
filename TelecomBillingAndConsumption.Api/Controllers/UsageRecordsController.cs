
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
        [HttpGet(Router.UsageRecords.getById)]
        public async Task<IActionResult> GetUsageRecordById(int id)
        {
            return Ok(await Mediator.Send(new GetUsageByIdQuery() { Id = id }));
        }

        [HttpGet(Router.UsageRecords.getBySubscriberId)]
        public async Task<IActionResult> GetUsageRecordsBySubscriberId(int subscriberId)
        {
            return Ok(await Mediator.Send(new GetUsageRecordsBySubscriberIdQuery() { SubscriberId = subscriberId }));
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
