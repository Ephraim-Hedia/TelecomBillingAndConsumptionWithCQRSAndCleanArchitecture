using TelecomBillingAndConsumption.Core.Features.ApplicationUser.Queries.Results;
using TelecomBillingAndConsumption.Data.Entities.Identity;

namespace TelecomBillingAndConsumption.Core.Mapping.ApplicationUserMapping
{
    public partial class ApplicationUserProfile
    {
        public void GetUserByIdMapping()
        {
            CreateMap<User, GetUserByIdResponse>();
        }
    }
}
