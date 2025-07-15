using MediatR;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Customers.Queries.GetCustomerFavoriteRestaurants
{
    public class GetCustomerFavoriteRestaurantsQuery : IRequest<List<RestaurantDto>>
    {
    }
}
