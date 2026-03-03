using MediatR;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.DashboardFeatures.Queries.Models;
using TelecomBillingAndConsumption.Core.Features.DashboardFeatures.Queries.Results;
using TelecomBillingAndConsumption.Core.Resources;

namespace TelecomBillingAndConsumption.Core.Features.DashboardFeatures.Queries.Handlers
{
    public class DashboardQueryHandler : ResponseHandler,
        IRequestHandler<GetDashboardOverviewQuery, Response<GetDashboardOverviewResponse>>,
        IRequestHandler<GetDashboardRevenueQuery, Response<GetDashboardRevenueResponse>>,
        IRequestHandler<GetUsageStatisticsQuery, Response<UsageStatisticsResponse>>,
        IRequestHandler<GetDashboardTopCustomersPaginatedQuery, List<GetDashboardTopCustomersPaginatedResponse>>
    {

        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Constructor
        public DashboardQueryHandler(IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _localizer = localizer;
        }

        public Task<Response<GetDashboardOverviewResponse>> Handle(GetDashboardOverviewQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Response<GetDashboardRevenueResponse>> Handle(GetDashboardRevenueQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Response<UsageStatisticsResponse>> Handle(GetUsageStatisticsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetDashboardTopCustomersPaginatedResponse>> Handle(GetDashboardTopCustomersPaginatedQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
