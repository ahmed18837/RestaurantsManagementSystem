using Restaurants.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace Restaurants.Application.Interfaces.Services
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> CreateTokenAsync(ApplicationUser user);
        RefreshToken GenerateRefreshToken();
        public string GenerateCode();
    }
}
