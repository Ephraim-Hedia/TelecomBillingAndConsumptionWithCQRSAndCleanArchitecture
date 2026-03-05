using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.DashboardFeatures.Queries.Models;
using TelecomBillingAndConsumption.Core.Features.DashboardFeatures.Queries.Results;
using TelecomBillingAndConsumption.Core.Resources;
using TelecomBillingAndConsumption.Service.Interfaces;

namespace TelecomBillingAndConsumption.Core.Features.DashboardFeatures.Queries.Handlers
{
    public class DashboardQueryHandler : ResponseHandler,
        IRequestHandler<GetDashboardOverviewQuery, Response<GetDashboardOverviewResponse>>,
        IRequestHandler<GetDashboardRevenueQuery, Response<GetDashboardRevenueResponse>>,
        IRequestHandler<GetUsageStatisticsQuery, Response<UsageStatisticsResponse>>,
        IRequestHandler<GetDashboardTopCustomersQuery, Response<List<GetDashboardTopCustomersResponse>>>
    {

        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IDashboardService _dashboardService;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public DashboardQueryHandler(
            IDashboardService dashboardService,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _mapper = mapper;
            _dashboardService = dashboardService;
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

        public async Task<Response<List<GetDashboardTopCustomersResponse>>> Handle(GetDashboardTopCustomersQuery request, CancellationToken cancellationToken)
        {
            var result = await _dashboardService.GetTopCustomersAsync(request.TopN);
            var mappedResponse = _mapper.Map<List<GetDashboardTopCustomersResponse>>(result);
            return Success(mappedResponse);
        }
        #endregion
    }
}
