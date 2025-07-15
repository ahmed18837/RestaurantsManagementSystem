using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.User;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Customers.Queries.GetCustomerFavoriteRestaurants
{
    public class GetCustomerFavoriteRestaurantsQueryHandler(ILogger<GetCustomerFavoriteRestaurantsQueryHandler> logger,
        ICustomersRepository customersRepository,
         ICustomerAuthorizationService customerAuthorizationService,
         IUserContext userContext,
         IMapper mapper) : IRequestHandler<GetCustomerFavoriteRestaurantsQuery, List<RestaurantDto>>
    {
        public async Task<List<RestaurantDto>> Handle(GetCustomerFavoriteRestaurantsQuery request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();

            if (currentUser is null || currentUser.CustomerId is null)
                throw new UnauthorizedAccessException("User must be logged in as a customer.");

            int customerId = currentUser.CustomerId.Value;

            var customer = await customersRepository.GetByIdWithFavoritesAsync(customerId, cancellationToken)
                          ?? throw new NotFoundException(nameof(Customer), customerId.ToString());

            if (!customerAuthorizationService.CanAddToFavorites(customer))
                throw new ForbidException();

            var favorites = await customersRepository.GetFavoriteRestaurantsAsync(customerId);

            logger.LogInformation("Customer with Id : {CustId} has {Count} favorite restaurants",
                                  customerId, favorites.Count);

            if (favorites == null || favorites.Count == 0)
                return [];

            return mapper.Map<List<RestaurantDto>>(favorites);
        }
    }
}
