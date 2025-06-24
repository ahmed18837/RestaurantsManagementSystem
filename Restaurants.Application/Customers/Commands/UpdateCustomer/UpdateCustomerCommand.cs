using MediatR;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Restaurants.Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest
    {
        [JsonIgnore]
        public int Id { get; set; }

        [DefaultValue("Ali Ali")]
        public string Name { get; set; } = default!;

        [DefaultValue("user@gmail.com")]
        public string Email { get; set; } = default!;

        [DefaultValue("01234567899")]
        public string PhoneNumber { get; set; } = default!;
    }
}
