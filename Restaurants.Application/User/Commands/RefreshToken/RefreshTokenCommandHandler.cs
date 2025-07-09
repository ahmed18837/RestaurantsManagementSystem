using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Restaurants.Application.Interfaces.Services;
using Restaurants.Application.User.Dtos;
using Restaurants.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace Restaurants.Application.User.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler(UserManager<ApplicationUser> userManager, ITokenService tokenService) : IRequestHandler<RefreshTokenCommand, ResponseDto>
    {

        public async Task<ResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var authModel = new ResponseDto();

            var user = userManager.Users.FirstOrDefault(u =>
                u.RefreshTokens.Any(t => t.Token == request.RefreshToken)) ?? throw new SecurityTokenException("Invalid refresh token");
            var refreshToken = user.RefreshTokens.First(t => t.Token == request.RefreshToken);

            if (!refreshToken.IsActive)
                throw new SecurityTokenException("Inactive refresh token");

            refreshToken.RevokedOn = DateTime.Now;

            var newRefreshToken = tokenService.GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);

            await userManager.UpdateAsync(user);

            var token = await tokenService.CreateTokenAsync(user);
            authModel.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
            authModel.RefreshToken = newRefreshToken.Token;
            authModel.RefreshTokenExpiration = newRefreshToken.ExpiresOn;

            return authModel;
        }
    }
}
