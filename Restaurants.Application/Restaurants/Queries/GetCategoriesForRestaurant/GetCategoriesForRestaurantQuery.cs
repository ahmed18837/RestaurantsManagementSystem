using MediatR;

namespace Restaurants.Application.Restaurants.Queries.GetCategoriesForRestaurant
{
    public class GetCategoriesForRestaurantQuery(int restaurantId) : IRequest<List<string>>
    {
        public int RestaurantId { get; } = restaurantId;
    }
}
