using MediatR;
using TelecomBillingAndConsumption.Core.Bases;

namespace TelecomBillingAndConsumption.Core.Features.TariffsFeatures.Commands.Models
{
    public class DeleteTariffRuleByIdCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
