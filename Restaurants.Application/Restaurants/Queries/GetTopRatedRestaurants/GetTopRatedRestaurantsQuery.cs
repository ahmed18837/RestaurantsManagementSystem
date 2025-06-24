using MediatR;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetTopRatedRestaurants
{
    public class GetTopRatedRestaurantsQuery(int count) : IRequest<List<RestaurantDto>>
    {
        public int Count { get; } = count;
    }

}
