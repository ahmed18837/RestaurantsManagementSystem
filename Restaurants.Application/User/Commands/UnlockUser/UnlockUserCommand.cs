using MediatR;
using System.ComponentModel;

namespace Restaurants.Application.User.Commands.UnlockUser
{
    public class UnlockUserCommand(string email) : IRequest<string>
    {
        [DefaultValue("user@gmail.com")]
        public string Email { get; } = email;
    }
}
