using MediatR;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.UsageFeatures.Queries.Models;
using TelecomBillingAndConsumption.Core.Features.UsageFeatures.Queries.Results;
using TelecomBillingAndConsumption.Core.Resources;
using TelecomBillingAndConsumption.Core.Wrappers;

namespace TelecomBillingAndConsumption.Core.Features.UsageFeatures.Queries.Handlers
{
    public class UsageQueryHandler : ResponseHandler
        , IRequestHandler<GetUsageByIdQuery, Response<GetUsageByIdResponse>>
        , IRequestHandler<GetUsageRecordsBySubscriberIdQuery, PaginatedResult<GetUsageRecordsBySubscriberIdResponse>>
        , IRequestHandler<GetUsageSummaryBySubscriberIdQuery, PaginatedResult<GetUsageSummaryBySubscriberIdResponse>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public UsageQueryHandler(IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _localizer = stringLocalizer;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<GetUsageByIdResponse>> Handle(GetUsageByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async Task<PaginatedResult<GetUsageRecordsBySubscriberIdResponse>> Handle(GetUsageRecordsBySubscriberIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedResult<GetUsageSummaryBySubscriberIdResponse>> Handle(GetUsageSummaryBySubscriberIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
