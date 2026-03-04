using MediatR;
using System.Text.Json.Serialization;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Data.Helpers;

namespace TelecomBillingAndConsumption.Core.Features.TariffsFeatures.Commands.Models
{
    public class AddTariffRuleCommand : IRequest<Response<int>>
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UsageType UsageType { get; set; }
        public bool IsRoaming { get; set; }
        public bool IsPeak { get; set; }
        public decimal PricePerUnit { get; set; }
    }
}
