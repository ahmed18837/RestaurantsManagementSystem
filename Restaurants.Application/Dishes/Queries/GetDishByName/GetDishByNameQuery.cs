using MediatR;
using Restaurants.Application.Dishes.Dtos;

namespace Restaurants.Application.Dishes.Queries.GetDishByName
{
    public class GetDishByNameQuery(string name) : IRequest<DishDto>
    {
        public string Name { get; } = name;
    }
}
