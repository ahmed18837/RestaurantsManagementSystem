using MediatR;

namespace Restaurants.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommand(int id) : IRequest
    {
        public int Id { get; } = id;
    }

}
