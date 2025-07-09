using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Restaurants.Application.Interfaces.Services;
using Restaurants.Application.User.Dtos;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace Restaurants.Application.User.Commands.LoginUser
{
    public class LoginUserCommandHandler(ILogger<LoginUserCommandHandler> logger,
        UserManager<ApplicationUser> userManager,
        IOptions<JwtOptions> jwt,
        ITokenService tokenService) : IRequestHandler<LoginUserCommand, ResponseDto>
    {
        private readonly JwtOptions jwt = jwt.Value;

        public async Task<ResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("JWT Config -> Key: {Key}, Issuer: {Issuer}, Audience: {Audience}", jwt.Key, jwt.Issuer, jwt.Audience);

            var authModel = new ResponseDto();

            var user = await userManager.FindByEmailAsync(request.Email) ??
                throw new Exception("UserName or Password is incorrect");

            var isValidPassword = await userManager.CheckPasswordAsync(user, request.Password);
            if (!isValidPassword)
                throw new Exception("UserName or Password is incorrect");

            var roles = await userManager.GetRolesAsync(user);
            if (roles == null || roles.Count == 0)
                throw new Exception("User has no assigned roles");

            var token = await tokenService.CreateTokenAsync(user);
            string encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

            authModel.AccessToken = encodedToken;

            if (user.RefreshTokens.Any(t => t.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                authModel.RefreshToken = activeRefreshToken!.Token;
                authModel.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;
            }
            else
            {
                var refreshToken = tokenService.GenerateRefreshToken();
                authModel.RefreshToken = refreshToken.Token;
                authModel.RefreshTokenExpiration = refreshToken.ExpiresOn;
                user.RefreshTokens.Add(refreshToken);
                await userManager.UpdateAsync(user);
            }

            return authModel;
        }
    }
}
