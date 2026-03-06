using System.IdentityModel.Tokens.Jwt;
using TelecomBillingAndConsumption.Data.Entities.Identity;
using TelecomBillingAndConsumption.Data.Results;

namespace TelecomBillingAndConsumption.Service.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResult> GetJwtToken(User user);
        public JwtSecurityToken ReadJWTToken(string accessToken);
        public Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string AccessToken, string RefreshToken);
        public Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken);
        public Task<string> ValidateToken(string AccessToken);
    }
}
