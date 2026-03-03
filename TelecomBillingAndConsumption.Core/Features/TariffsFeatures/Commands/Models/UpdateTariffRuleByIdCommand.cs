using MediatR;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Data.Helpers;

namespace TelecomBillingAndConsumption.Core.Features.TariffsFeatures.Commands.Models
{
    public class UpdateTariffRuleByIdCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

        public UsageType UsageType { get; set; }

        public bool IsRoaming { get; set; }

        public bool IsPeak { get; set; }

        public decimal PricePerUnit { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }
    }
}
