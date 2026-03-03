using MediatR;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Data.Helpers;

namespace TelecomBillingAndConsumption.Core.Features.TariffsFeatures.Commands.Models
{
    public class AddTariffRuleCommand : IRequest<Response<int>>
    {
        public UsageType UsageType { get; set; }

        public bool IsRoaming { get; set; }

        public bool IsPeak { get; set; }

        public decimal PricePerUnit { get; set; }

        public DateTime EffectiveFrom { get; set; }
    }
}
