using MediatR;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Models;
using TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Results;
using TelecomBillingAndConsumption.Core.Resources;

namespace TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Handlers
{
    public class BillingQueryHandler : ResponseHandler,
        IRequestHandler<GetBillByIdQuery, Response<GetBillByIdResponse>>,
        IRequestHandler<GetBillingHistoryForSubscriberQuery, List<GetBillingHistoryForSubscriberResponse>>,
        IRequestHandler<GetBillingDetailsByBillIdQuery, List<GetBillingDetailsByBillIdResponse>>,
        IRequestHandler<GetAllBillingsBySubscriberIdQuery, List<GetAllBillingsBySubscriberIdResponse>>

    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructor

        public BillingQueryHandler(IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _localizer = stringLocalizer;
        }
        #endregion

        #region Handlers
        public Task<Response<GetBillByIdResponse>> Handle(GetBillByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetBillingHistoryForSubscriberResponse>> Handle(GetBillingHistoryForSubscriberQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetBillingDetailsByBillIdResponse>> Handle(GetBillingDetailsByBillIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetAllBillingsBySubscriberIdResponse>> Handle(GetAllBillingsBySubscriberIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
