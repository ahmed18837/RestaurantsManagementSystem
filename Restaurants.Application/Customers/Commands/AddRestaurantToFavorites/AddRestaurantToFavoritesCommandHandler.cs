using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.User;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Customers.Commands.AddRestaurantToFavorites
{
    public class AddRestaurantToFavoritesCommandHandler(ILogger<AddRestaurantToFavoritesCommandHandler> logger,
        ICustomersRepository customersRepository,
        ICustomerAuthorizationService customerAuthorizationService,
        IUserContext userContext,
        IRestaurantsRepository restaurantsRepository) : IRequestHandler<AddRestaurantToFavoritesCommand>
    {
        public async Task Handle(AddRestaurantToFavoritesCommand request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();

            if (currentUser is null || currentUser.CustomerId is null)
                throw new UnauthorizedAccessException("User must be logged in as a customer.");

            int customerId = currentUser.CustomerId.Value;

            var customer = await customersRepository.GetByIdWithFavoritesAsync(customerId, cancellationToken)
                ?? throw new NotFoundException(nameof(Customer), customerId.ToString());

            var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
                ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            if (customer.FavoriteRestaurants.Any(r => r.Id == request.RestaurantId))
                throw new BadRequestException($"Restaurant with id {request.RestaurantId} is already in customer's favorites.");

            logger.LogInformation("Auth Fav | user.Id={Uid} user.CustId={Cid} cust.AppUserId={CAU} cust.Id={CustId}",
                      currentUser.Id, currentUser.CustomerId, customer.ApplicationUserId, customer.Id);

            if (!customerAuthorizationService.CanAddToFavorites(customer))
                throw new ForbidException();

            await customersRepository.AddFavoriteRestaurantAsync(customerId, request.RestaurantId);

            logger.LogInformation("Add a Restaurant To Favorite {@RestaurantId}", request.RestaurantId);
        }
    }
}
