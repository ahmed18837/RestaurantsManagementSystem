using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace Restaurants.Application.User.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<IdentityResult>
    {
        [DefaultValue("FullName")]
        public string FullName { get; set; } = default!;

        [DefaultValue("user@gmail.com")]
        public string Email { get; set; } = default!;

        [DefaultValue("+201234567890")]
        public string PhoneNumber { get; set; } = default!;

        [DefaultValue("Admin")]
        public string UserType { get; set; } = default!;

        [DefaultValue("SecurePass@123")]
        public string Password { get; set; } = default!;
    }
}
