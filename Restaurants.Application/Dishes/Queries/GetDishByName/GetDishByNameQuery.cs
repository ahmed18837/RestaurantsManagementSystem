using MediatR;
using Restaurants.Application.Dishes.Dtos;

namespace Restaurants.Application.Dishes.Queries.GetDishByName
{
    public class GetDishByNameQuery(int restaurantId, string name) : IRequest<DishDto>
    {
        public int RestaurantId { get; } = restaurantId;
        public string Name { get; } = name;
    }
}
