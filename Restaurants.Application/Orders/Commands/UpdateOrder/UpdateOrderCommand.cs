using MediatR;
using Restaurants.Application.Orders.Dtos;
using System.Text.Json.Serialization;

namespace Restaurants.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest
    {
        [JsonIgnore]
        public int Id { get; set; }

        public IList<UpdateOrderItemDto> Items { get; set; } = [];
    }
}
