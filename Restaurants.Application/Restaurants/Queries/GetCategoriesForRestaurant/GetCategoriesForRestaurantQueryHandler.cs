using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetCategoriesForRestaurant
{
    public class GetCategoriesForRestaurantQueryHandler(
        ILogger<GetCategoriesForRestaurantQueryHandler> logger, IRestaurantsRepository restaurantsRepository)
        : IRequestHandler<GetCategoriesForRestaurantQuery, List<string>>
    {
        public async Task<List<string>> Handle(GetCategoriesForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting categories for restaurant {RestaurantId}", request.RestaurantId);

            var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
                    ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var categories = await restaurantsRepository.GetCategoriesForRestaurantAsync(request.RestaurantId);

            if (categories == null || categories.Count == 0)
                return [];

            return categories;
        }
    }
}
