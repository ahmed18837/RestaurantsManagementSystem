using MediatR;
using Restaurants.Application.User.Dtos;
using System.ComponentModel;

namespace Restaurants.Application.User.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<ResponseDto>
    {
        [DefaultValue("user@gmail.com")]
        public string Email { get; set; } = default!;

        [DefaultValue("SecurePass@123")]
        public string Password { get; set; } = default!;
    }
}
