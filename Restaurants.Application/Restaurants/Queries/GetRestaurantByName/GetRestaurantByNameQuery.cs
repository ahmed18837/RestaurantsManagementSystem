using MediatR;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantByName
{
    public class GetRestaurantByNameQuery(string name) : IRequest<RestaurantDto>
    {
        public string Name { get; } = name;
    }
}
