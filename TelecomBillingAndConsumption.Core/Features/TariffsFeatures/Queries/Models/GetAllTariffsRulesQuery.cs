using MediatR;
using TelecomBillingAndConsumption.Core.Features.TariffsFeatures.Queries.Results;

namespace TelecomBillingAndConsumption.Core.Features.TariffsFeatures.Queries.Models
{
    public class GetAllTariffsRulesQuery : IRequest<List<GetAllTariffsRulesResponse>>
    {

    }
}
