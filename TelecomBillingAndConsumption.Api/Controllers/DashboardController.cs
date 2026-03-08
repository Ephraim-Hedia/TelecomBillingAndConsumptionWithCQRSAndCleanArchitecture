using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelecomBillingAndConsumption.Api.Bases;
using TelecomBillingAndConsumption.Core.Features.DashboardFeatures.Queries.Models;
using TelecomBillingAndConsumption.Data.AppMetaData;

namespace TelecomBillingAndConsumption.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = ("Admin"))]
    public class DashboardController : AppControllerBase
    {
        // GetDashboardOverviewQuery
        // GetDashboardRevenueQuery
        // GetDashboardTopCustomersPaginatedQuery
        // GetUsageStatisticsQuery
        [HttpGet]
        [Route(Router.DashboardRouting.getOverview)]
        public async Task<IActionResult> GetDashboardOverview(GetDashboardOverviewQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet]
        [Route(Router.DashboardRouting.getRevenue)]
        public async Task<IActionResult> GetDashboardRevenue([FromQuery] int month, [FromQuery] int year, [FromQuery] string responseFormat = "json")
        {
            var result = await Mediator.Send(new GetDashboardRevenueQuery() { Month = month, Year = year });
            if (responseFormat.ToLower() == "soap" || responseFormat.ToLower() == "xml")
            {
                var xml = SerializeToSoap.Serialize(result);
                return Content(xml, "application/soap+xml");
            }

            return Ok(result);
        }
        [HttpGet]
        [Route(Router.DashboardRouting.getUsageStatistics)]
        public async Task<IActionResult> GetUsageStatistics([FromQuery] int month, [FromQuery] int year, [FromQuery] string responseFormat = "json")
        {
            var result = await Mediator.Send(new GetUsageStatisticsQuery() { Month = month, Year = year });
            if (responseFormat.ToLower() == "soap" || responseFormat.ToLower() == "xml")
            {
                var xml = SerializeToSoap.Serialize(result);
                return Content(xml, "application/soap+xml");
            }

            return Ok(result);
        }
        [HttpGet]
        [Route(Router.DashboardRouting.getTopCustomers)]
        public async Task<IActionResult> GetTopCustomersPaginated([FromQuery] int topN = 10, [FromQuery] string responseFormat = "json")
        {
            var result = await Mediator.Send(new GetDashboardTopCustomersQuery() { TopN = topN });
            if (responseFormat.ToLower() == "soap" || responseFormat.ToLower() == "xml")
            {
                var xml = SerializeToSoap.Serialize(result);
                return Content(xml, "application/soap+xml");
            }

            return Ok(result);
        }
    }
}
