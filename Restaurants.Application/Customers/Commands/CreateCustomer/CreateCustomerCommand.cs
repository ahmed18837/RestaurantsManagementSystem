using MediatR;
using System.ComponentModel;

namespace Restaurants.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<int>
    {
        [DefaultValue("Ahmed Ali")]
        public string Name { get; set; } = default!;

        [DefaultValue("user@gmail.com")]
        public string Email { get; set; } = default!;

        [DefaultValue("01234567891")]
        public string PhoneNumber { get; set; } = default!;

        [DefaultValue("12345678Aa@")]
        public string Password { get; set; } = default!;
    }
}
