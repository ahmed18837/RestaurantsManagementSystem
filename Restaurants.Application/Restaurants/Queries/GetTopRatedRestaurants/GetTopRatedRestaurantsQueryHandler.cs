using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetTopRatedRestaurants
{
    public class GetTopRatedRestaurantsQueryHandler(ILogger<GetTopRatedRestaurantsQueryHandler> logger,
        IRestaurantsRepository repository, IMapper mapper) : IRequestHandler<GetTopRatedRestaurantsQuery, List<RestaurantDto>>
    {
        public async Task<List<RestaurantDto>> Handle(GetTopRatedRestaurantsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting Toped Restaurants {RestaurantCount}", request.Count);

            var topRated = await repository.GetTopRatedAsync(request.Count);

            if (topRated == null || topRated.Count == 0)
                return [];

            return mapper.Map<List<RestaurantDto>>(topRated);
        }
    }

}
