using AutoMapper;
using MediatR;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Customers.Queries.GetCustomerFavoriteRestaurants
{
    public class GetCustomerFavoriteRestaurantsQueryHandler(ICustomersRepository customersRepository, IMapper mapper) : IRequestHandler<GetCustomerFavoriteRestaurantsQuery, List<RestaurantDto>>
    {
        public async Task<List<RestaurantDto>> Handle(GetCustomerFavoriteRestaurantsQuery request, CancellationToken cancellationToken)
        {
            var favorites = await customersRepository.GetFavoriteRestaurantsAsync(request.CustomerId);

            if (favorites == null || favorites.Count == 0)
                return [];

            return mapper.Map<List<RestaurantDto>>(favorites);
        }
    }
}
