using MediatR;
using Restaurants.Application.Customers.Commands.CreateCustomer;

namespace Restaurants.Application.Customers.Commands.CreateMultipleCustomers
{
    public class CreateMultipleCustomersCommand : IRequest<List<int>>
    {
        public List<CreateCustomerCommand> Customers { get; set; } = new();
    }

}
