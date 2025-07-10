using MediatR;
using System.ComponentModel;

namespace Restaurants.Application.User.Commands.ResetPassword
{
    public class ResetPasswordCommand(string email) : IRequest<string>
    {
        [DefaultValue("user@gmail.com")]
        public string Email { get; } = email;

        [DefaultValue("123456")]
        public string ResetCode { get; set; } = default!;

        [DefaultValue("SecurePass123")]
        public string NewPassword { get; set; } = default!;
    }
}
