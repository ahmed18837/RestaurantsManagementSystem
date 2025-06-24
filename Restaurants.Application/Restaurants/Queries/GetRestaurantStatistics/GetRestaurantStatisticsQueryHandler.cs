using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantStatistics
{
    public class GetRestaurantStatisticsQueryHandler(ILogger<GetRestaurantStatisticsQueryHandler> logger,
        IRestaurantsRepository restaurantsRepository, IMapper mapper)
    : IRequestHandler<GetRestaurantStatisticsQuery, RestaurantStatisticsDto>
    {
        public async Task<RestaurantStatisticsDto> Handle(GetRestaurantStatisticsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting Statistics For Restaurant {RestaurantId}", request.RestaurantId);

            var restaurant = await restaurantsRepository.GetStatisticsAsync(request.RestaurantId)
                ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var restaurantStatisticsDto = mapper.Map<RestaurantStatisticsDto>(restaurant);

            restaurantStatisticsDto.TotalOrders = await restaurantsRepository.GetTotalOrdersForRestaurantAsync(request.RestaurantId);

            restaurantStatisticsDto.TotalRevenue =
                await restaurantsRepository.GetTotalRevenueForRestaurantAsync(request.RestaurantId);

            restaurantStatisticsDto.MostPopularDishName =
                await restaurantsRepository.GetMostPopularDishNameAsync(request.RestaurantId);

            restaurantStatisticsDto.LastOrderDate =
                await restaurantsRepository.GetLastOrderDateAsync(request.RestaurantId);

            return restaurantStatisticsDto;
        }
    }
}
