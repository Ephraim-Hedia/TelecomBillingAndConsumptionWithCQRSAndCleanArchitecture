using TelecomBillingAndConsumption.Core.Features.ApplicationUser.Queries.Results;
using TelecomBillingAndConsumption.Data.Entities.Identity;

namespace TelecomBillingAndConsumption.Core.Mapping.ApplicationUserMapping
{
    public partial class ApplicationUserProfile
    {
        public void GetUserPaginatedMapping()
        {
            CreateMap<User, GetUserPaginatedResponse>();
        }
    }
}
