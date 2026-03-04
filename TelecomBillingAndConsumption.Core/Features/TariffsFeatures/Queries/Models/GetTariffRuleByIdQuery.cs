using MediatR;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.TariffsFeatures.Queries.Results;

namespace TelecomBillingAndConsumption.Core.Features.TariffsFeatures.Queries.Models
{
    public class GetTariffRuleByIdQuery : IRequest<Response<GetTariffRuleByIdResponse>>
    {
        public int Id { get; set; }
        public GetTariffRuleByIdQuery(int id) => Id = id;

    }
}
