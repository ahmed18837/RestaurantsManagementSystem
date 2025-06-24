using MediatR;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantStatistics
{
    public class GetRestaurantStatisticsQuery(int restaurantId) : IRequest<RestaurantStatisticsDto>
    {
        public int RestaurantId { get; } = restaurantId;
    }
}
