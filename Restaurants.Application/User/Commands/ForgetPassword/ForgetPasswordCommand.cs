using MediatR;
using System.ComponentModel;

namespace Restaurants.Application.User.Commands.ForgetPassword
{
    public class ForgetPasswordCommand(string email) : IRequest<string>
    {
        [DefaultValue("user@gmail.com")]
        public string Email { get; } = email;
    }
}
