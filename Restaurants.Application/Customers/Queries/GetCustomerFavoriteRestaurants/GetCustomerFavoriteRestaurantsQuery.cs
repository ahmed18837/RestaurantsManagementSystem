using MediatR;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Customers.Queries.GetCustomerFavoriteRestaurants
{
    public class GetCustomerFavoriteRestaurantsQuery(int customerId) : IRequest<List<RestaurantDto>>
    {
        public int CustomerId { get; set; } = customerId;
    }
}
