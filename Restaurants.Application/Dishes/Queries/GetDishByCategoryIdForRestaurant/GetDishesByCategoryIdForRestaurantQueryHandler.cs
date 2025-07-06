using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishByCategoryIdForRestaurant
{
    public class GetDishesByCategoryIdForRestaurantQueryHandler(ILogger<GetDishesByCategoryIdForRestaurantQueryHandler> logger,
    IDishesRepository dishesRepository,
    IRestaurantsRepository restaurantsRepository,
    ICategoriesRepository categoriesRepository,
    IMapper mapper) : IRequestHandler<GetDishesByCategoryIdForRestaurantQuery, PagedResult<DishDto>>
    {
        async Task<PagedResult<DishDto>> IRequestHandler<GetDishesByCategoryIdForRestaurantQuery, PagedResult<DishDto>>.Handle(GetDishesByCategoryIdForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving All dishes For {categoryId} : ", request.CategoryId);

            var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
             ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var category = await categoriesRepository.GetByIdAsync(request.CategoryId)
                ?? throw new NotFoundException(nameof(Category), request.CategoryId.ToString());

            var (dishes, totalCount) = await dishesRepository.GetAllMatchingAsync(
                request.RestaurantId,
                request.SearchPhrase,
               request.PageSize,
               request.PageNumber,
               request.SortBy,
               request.SortDirection);

            var dishesForCategory = dishes.Where(d => d.CategoryId == request.CategoryId
            && d.RestaurantId == request.RestaurantId);

            if (!dishesForCategory.Any())
            {
                throw new NotFoundException(nameof(Dish), $"No dishes found for category {request.CategoryId} in restaurant {request.RestaurantId}");
            }

            var dishesDtos = mapper.Map<IEnumerable<DishDto>>(dishesForCategory);

            var result = new PagedResult<DishDto>(dishesDtos, totalCount, request.PageSize, request.PageNumber);
            return result;
        }
    }

}
