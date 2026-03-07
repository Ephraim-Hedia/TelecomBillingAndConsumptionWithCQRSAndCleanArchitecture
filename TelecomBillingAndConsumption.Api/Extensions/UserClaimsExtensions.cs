using System.Security.Claims;

namespace TelecomBillingAndConsumption.Api.Extensions
{
    public static class UserClaimsExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var value = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return string.IsNullOrEmpty(value) ? 0 : int.Parse(value);
        }

        public static int? GetSubscriberId(this ClaimsPrincipal user)
        {
            var value = user.FindFirst("SubscriberId")?.Value;
            return string.IsNullOrEmpty(value) ? null : int.Parse(value);
        }

        public static string GetUserRole(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Role)?.Value ?? "";
        }

        public static string GetSubscriberPhone(this ClaimsPrincipal user)
        {
            return user.FindFirst("SubscriberPhone")?.Value ?? "";
        }
    }
}
