using MediatR;
using Restaurants.Application.Categories.Dtos;

namespace Restaurants.Application.Categories.Queries.GetCategoryByName
{
    public class GetCategoryByNameQuery(string name) : IRequest<CategoryDto>
    {
        public string Name { get; } = name;
    }
}
