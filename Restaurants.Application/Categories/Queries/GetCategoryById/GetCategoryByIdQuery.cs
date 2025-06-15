using MediatR;
using Restaurants.Application.Categories.Dtos;

namespace Restaurants.Application.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQuery(int categoryId) : IRequest<CategoryDto>
    {
        public int CategoryId { get; } = categoryId;
    }

}
