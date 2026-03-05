using TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Results;
using TelecomBillingAndConsumption.Data.Entities;

namespace TelecomBillingAndConsumption.Core.Mapping.BillingMapping
{
    public partial class BillingProfile
    {
        public void GetAllBillingsBySubscriberIdMapping()
        {
            CreateMap<Bill, GetAllBillingsBySubscriberIdResponse>();
        }
    }
}
