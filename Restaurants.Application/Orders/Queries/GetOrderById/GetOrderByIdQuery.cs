using MediatR;
using Restaurants.Application.Orders.Dtos;

namespace Restaurants.Application.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQuery(int id) : IRequest<OrderDto>
    {
        public int Id { get; } = id;
    }
}
