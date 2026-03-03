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
        public async Task<IActionResult> GetDashboardRevenue(GetDashboardRevenueQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpGet]
        [Route(Router.DashboardRouting.getUsageStatistics)]
        public async Task<IActionResult> GetUsageStatistics(GetUsageStatisticsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpGet]
        [Route(Router.DashboardRouting.getTopCustomersPaginated)]
        public async Task<IActionResult> GetTopCustomersPaginated([FromQuery] GetDashboardTopCustomersPaginatedQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
