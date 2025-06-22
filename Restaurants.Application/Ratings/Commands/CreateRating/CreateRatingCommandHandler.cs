using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Ratings.Commands.CreateRating;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRatingCommandHandler(ILogger<CreateRatingCommandHandler> logger,
        IMapper mapper,
        IRatingsRepository ratingsRepository, IDishesRepository dishesRepository,
        IRestaurantsRepository restaurantsRepository, ICustomersRepository customersRepository) : IRequestHandler<CreateRatingCommand, int>
    {
        public async Task<int> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("creating a new Rating {@Rating}", request);

            var restaurant = await restaurantsRepository.GetByNameAsync(request.RestaurantName)
                   ?? throw new NotFoundNameException(nameof(Restaurant), request.RestaurantName);

            var dish = await dishesRepository.GetByNameAsync(request.DishName)
                  ?? throw new NotFoundException(nameof(Dish), request.DishName);

            var customer = await customersRepository.GetByNameAsync(request.CustomerName)
                   ?? throw new NotFoundNameException(nameof(Customer), request.CustomerName);

            //var rating = mapper.Map<Rating>(request);

            var rating = new Rating
            {
                Stars = request.Stars,
                Comment = request.Comment,
                DishId = dish.Id,
                RestaurantId = restaurant.Id,
                CustomerId = customer.Id
            };

            await ratingsRepository.AddAsync(rating);

            return rating.Id;
        }
    }

}
