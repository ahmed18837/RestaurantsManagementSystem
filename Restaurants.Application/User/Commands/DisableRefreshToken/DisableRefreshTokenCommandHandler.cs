using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Restaurants.Application.User.Dtos;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.User.Commands.DisableRefreshToken
{
    public class DisableRefreshTokenCommandHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<DisableRefreshTokenCommand, ResponseDto>
    {

        public async Task<ResponseDto> Handle(DisableRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();

            var user = userManager.Users.FirstOrDefault(u =>
                u.RefreshTokens.Any(t => t.Token == request.RefreshToken))
                ?? throw new SecurityTokenException("Invalid refresh token");

            var token = user.RefreshTokens.FirstOrDefault(t => t.Token == request.RefreshToken);

            if (token == null || !token.IsActive)
                throw new SecurityTokenException("Refresh token is already inactive");

            token.RevokedOn = DateTime.Now;

            await userManager.UpdateAsync(user);

            response.Message = "Refresh token disabled successfully";
            return response;
        }
    }
}
