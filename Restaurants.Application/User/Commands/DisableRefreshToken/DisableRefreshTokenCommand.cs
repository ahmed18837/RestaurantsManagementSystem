using MediatR;
using Restaurants.Application.User.Dtos;

namespace Restaurants.Application.User.Commands.DisableRefreshToken
{
    public class DisableRefreshTokenCommand : IRequest<ResponseDto>
    {
        public string RefreshToken { get; set; } = default!;
    }
}
