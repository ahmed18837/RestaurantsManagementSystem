using MediatR;
using Restaurants.Application.User.Dtos;

namespace Restaurants.Application.User.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<ResponseDto>
    {
        public string RefreshToken { get; set; } = default!;
    }
}
