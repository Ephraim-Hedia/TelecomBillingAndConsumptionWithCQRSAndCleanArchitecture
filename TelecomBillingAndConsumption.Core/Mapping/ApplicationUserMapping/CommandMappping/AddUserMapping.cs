using TelecomBillingAndConsumption.Core.Features.ApplicationUser.Commands.Models;
using TelecomBillingAndConsumption.Data.Entities.Identity;

namespace TelecomBillingAndConsumption.Core.Mapping.ApplicationUserMapping
{
    public partial class ApplicationUserProfile
    {
        public void AddUserMapping()
        {
            CreateMap<AddUserCommand, User>();
        }
    }
}
