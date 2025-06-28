using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Customers.Commands.AddRestaurantToFavorites
{
    public class AddRestaurantToFavoritesCommandHandler(ILogger<AddRestaurantToFavoritesCommandHandler> logger,
        ICustomersRepository customersRepository,
        IRestaurantsRepository restaurantsRepository) : IRequestHandler<AddRestaurantToFavoritesCommand>
    {
        public async Task Handle(AddRestaurantToFavoritesCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Add a Restaurant To Favorite {@RestaurantId}", request.RestaurantId);

            var customer = await customersRepository.GetByIdAsync(request.CustomerId)
               ?? throw new NotFoundException(nameof(Customer), request.CustomerId.ToString());

            var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
               ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            if (customer.FavoriteRestaurants.Any(r => r.Id == request.RestaurantId))
            {
                throw new Exception($"Restaurant with id {request.RestaurantId} is already in customer's favorites.");
            }

            await customersRepository.AddFavoriteRestaurantAsync(request.CustomerId, request.RestaurantId);
        }
    }
}
