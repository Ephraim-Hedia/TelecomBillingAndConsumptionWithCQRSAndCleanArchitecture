using MediatR;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Data.Helpers;

namespace TelecomBillingAndConsumption.Core.Features.UsageFeatures.Commands.Models
{
    public class AddUsageRecordCommand : IRequest<Response<int>>
    {
        public int SubscriberId { get; set; }

        public UsageType UsageType { get; set; }

        public int? CallMinutes { get; set; }

        public decimal? DataMB { get; set; }

        public int? SMSCount { get; set; }

        public bool IsRoaming { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
