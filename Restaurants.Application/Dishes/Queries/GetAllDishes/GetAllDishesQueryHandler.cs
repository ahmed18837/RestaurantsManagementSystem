using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetAllDishes
{
    public class GetAllDishesQueryHandler(ILogger<GetAllDishesQueryHandler> logger,
    IDishesRepository dishesRepository,
    IRestaurantsRepository restaurantsRepository,
    IMapper mapper) : IRequestHandler<GetAllDishesQuery, PagedResult<DishDto>>
    {
        async Task<PagedResult<DishDto>> IRequestHandler<GetAllDishesQuery, PagedResult<DishDto>>.Handle(GetAllDishesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving All dishes");

            var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
                ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var (dishes, totalCount) = await dishesRepository.GetAllMatchingAsync(
                request.RestaurantId,
                request.SearchPhrase,
               request.PageSize,
               request.PageNumber,
               request.SortBy,
               request.SortDirection);

            var dishesDtos = mapper.Map<IEnumerable<DishDto>>(dishes);

            var result = new PagedResult<DishDto>(dishesDtos, totalCount, request.PageSize, request.PageNumber);
            return result;
        }
    }

}
