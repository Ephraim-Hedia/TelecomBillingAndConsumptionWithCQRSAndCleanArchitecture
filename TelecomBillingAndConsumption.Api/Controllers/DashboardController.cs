using Microsoft.AspNetCore.Mvc;
using TelecomBillingAndConsumption.Api.Bases;
using TelecomBillingAndConsumption.Core.Features.DashboardFeatures.Queries.Models;
using TelecomBillingAndConsumption.Data.AppMetaData;

namespace TelecomBillingAndConsumption.Api.Controllers
{
    [ApiController]
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
        public async Task<IActionResult> GetDashboardRevenue([FromQuery] int month, [FromQuery] int Year)
        {
            return Ok(await Mediator.Send(new GetDashboardRevenueQuery() { Month = month, Year = Year }));
        }
        [HttpGet]
        [Route(Router.DashboardRouting.getUsageStatistics)]
        public async Task<IActionResult> GetUsageStatistics(GetUsageStatisticsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpGet]
        [Route(Router.DashboardRouting.getTopCustomers)]
        public async Task<IActionResult> GetTopCustomersPaginated([FromQuery] int topN = 10)
        {
            var result = await Mediator.Send(new GetDashboardTopCustomersQuery() { TopN = topN });
            return Ok(result);
        }
    }
}
