using MediatR;
using System.ComponentModel;

namespace Restaurants.Application.User.Commands.Send2FACode
{
    public class Send2FACodeCommand(string email) : IRequest<string>
    {
        [DefaultValue("user@gmail.com")]
        public string Email { get; } = email;
    }
}
