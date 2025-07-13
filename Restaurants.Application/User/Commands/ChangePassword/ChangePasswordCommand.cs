using MediatR;
using System.ComponentModel;

namespace Restaurants.Application.User.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<string>
    {
        [DefaultValue("user@gmail.com")]
        public string Email { get; set; } = default!;

        [DefaultValue("SecurePass123")]
        public string CurrentPassword { get; set; } = default!;

        [DefaultValue("SecurePass123")]
        public string NewPassword { get; set; } = default!;
    }
}
