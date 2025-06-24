using MediatR;
using Restaurants.Application.Orders.Dtos;
using System.ComponentModel;

namespace Restaurants.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<int>
    {
        [DefaultValue(500)]
        public int CustomerId { get; set; }

        public List<CreateOrderItemDto> Items { get; set; } = [];
    }
}
