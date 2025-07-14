using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Interfaces.Services;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDish
{
    public class DeleteDishCommandHandler(ILogger<DeleteDishCommandHandler> logger,
    IDishesRepository dishesRepository,
     IDishAuthorizationService dishAuthorizationService,
    IRestaurantsRepository restaurantsRepository,
    IFileService fileService) : IRequestHandler<DeleteDishCommand>
    {
        public async Task Handle(DeleteDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting Dish with id: {DishId}", request.Id);

            _ = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
           ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var dish = await dishesRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Dish), request.Id.ToString());

            if (dish.RestaurantId != request.RestaurantId)
                throw new NotFoundException(nameof(Dish), request.Id.ToString());

            if (!dishAuthorizationService.Authorize(dish, ResourceOperation.Delete))
                throw new ForbidException();

            if (!string.IsNullOrWhiteSpace(dish.ImageFileName))
                _ = fileService.DeleteFileAsync(dish.ImageFileName);

            await dishesRepository.DeleteAsync(dish);
        }
    }
}
