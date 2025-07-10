using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Interfaces.Services;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
        IDishesRepository dishesRepository,
        ICategoriesRepository categoriesRepository,
        IRestaurantsRepository restaurantsRepository,
        IFileService fileService) : IRequestHandler<CreateDishCommand, int>
    {
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new Dish {@Dish}", request);

            var existCategory = await categoriesRepository.GetByNameAsync(request.CategoryName)
                 ?? throw new NotFoundNameException(nameof(Dish), request.CategoryName);

            var existRestaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
                 ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var dish = new Dish
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                ImageFileName = request.Image != null ? fileService.SaveFile(request.Image, nameof(Dish)) : null,
                RestaurantId = request.RestaurantId,
                CategoryId = existCategory!.Id
            };

            await dishesRepository.AddAsync(dish);

            return dish.Id;
        }
    }

}
