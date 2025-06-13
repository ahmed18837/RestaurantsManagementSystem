using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant
{
    public class GetDishesForRestaurantQueryHandler(ILogger<GetDishesForRestaurantQueryHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository dishesRepository,
    IMapper mapper) : IRequestHandler<GetDishesForRestaurantQuery, PagedResult<DishDto>>
    {
        public async Task<PagedResult<DishDto>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving dishes for restaurant with id: {RestaurantId}", request.RestaurantId);

            var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
                ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var (dishes, totalCount) = await dishesRepository.GetAllMatchingAsync(request.SearchPhrase,
               request.PageSize,
               request.PageNumber,
               request.SortBy,
               request.SortDirection);

            var dishesForRestaurant = dishes.Where(d => d.RestaurantId == request.RestaurantId);

            var dishesDtos = mapper.Map<IEnumerable<DishDto>>(dishesForRestaurant);
            //var dishesDtos = mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes); // If Use Include In Repo (GetByIdAsync For Restaurant)

            var result = new PagedResult<DishDto>(dishesDtos, totalCount, request.PageSize, request.PageNumber);
            return result;
        }
    }

}
