using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishByName
{
    public class GetDishByNameQueryHandler(ILogger<GetDishByNameQueryHandler> logger,
     IDishesRepository dishesRepository,
     IRestaurantsRepository restaurantsRepository,
     IMapper mapper) : IRequestHandler<GetDishByNameQuery, DishDto>
    {
        public async Task<DishDto> Handle(GetDishByNameQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting Dish {DishName}", request.Name);

            _ = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
          ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var dish = await dishesRepository.GetByNameAsync(request.Name)
                    ?? throw new NotFoundNameException(nameof(Dish), request.Name);

            if (dish.RestaurantId != request.RestaurantId)
                throw new NotFoundNameException(nameof(Dish), request.Name);

            var dishDto = mapper.Map<DishDto>(dish);

            return dishDto;
        }
    }
}
