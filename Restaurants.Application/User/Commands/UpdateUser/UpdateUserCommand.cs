using MediatR;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Restaurants.Application.User.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {
        [JsonIgnore]
        public string? Id { get; set; }

        [DefaultValue("FullName")]
        public string FullName { get; set; } = default!;

        [DefaultValue("user@gmail.com")]
        public string Email { get; set; } = default!;

        [DefaultValue("01234567891")]
        public string PhoneNumber { get; set; } = default!;

        [DefaultValue("Admin")]
        public string UserType { get; set; } = default!;
    }
}
