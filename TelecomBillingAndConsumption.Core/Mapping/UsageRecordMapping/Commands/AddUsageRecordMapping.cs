using TelecomBillingAndConsumption.Core.Features.UsageFeatures.Commands.Models;
using TelecomBillingAndConsumption.Data.Entities;

namespace TelecomBillingAndConsumption.Core.Mapping.UsageRecordMapping
{
    public partial class UsageRecordProfile
    {
        public void AddUsageRecordMapping()
        {
            CreateMap<AddUsageRecordCommand, UsageRecord>().ReverseMap();
        }
    }
}
