using MediatR;
using System.Text.Json.Serialization;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Data.Helpers;

namespace TelecomBillingAndConsumption.Core.Features.UsageFeatures.Commands.Models
{
    public class AddUsageRecordCommand : IRequest<Response<int>>
    {
        public int SubscriberId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UsageType UsageType { get; set; }

        public int? CallMinutes { get; set; }

        public decimal? DataMB { get; set; }

        public int? SMSCount { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
