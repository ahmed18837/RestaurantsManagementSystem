using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant
{
    public class GetDishByIdForRestaurantQueryHandler(
    ILogger<GetDishByIdForRestaurantQueryHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository dishesRepository,
    IMapper mapper) : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
    {
        public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving dish: {DishId}, for restaurant with id: {RestaurantId}",
                request.DishId,
                request.RestaurantId);

            var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
                ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            //var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId)
            //    ?? throw new NotFoundException(nameof(Dish), request.DishId.ToString()); // If Use Include In Repo (GetByIdAsync For Restaurant)

            var dish = await dishesRepository.GetByIdIncludeRestaurantAndCategory(request.DishId)
                ?? throw new NotFoundException(nameof(Dish), request.DishId.ToString());

            var result = mapper.Map<DishDto>(dish);

            return result;
        }
    }

}
