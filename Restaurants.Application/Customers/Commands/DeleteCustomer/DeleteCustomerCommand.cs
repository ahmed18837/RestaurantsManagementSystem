using MediatR;

namespace Restaurants.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand(int id) : IRequest
    {
        public int Id { get; } = id;
    }

}
