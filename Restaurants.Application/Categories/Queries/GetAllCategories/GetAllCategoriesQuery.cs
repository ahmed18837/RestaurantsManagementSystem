using MediatR;
using Restaurants.Application.Categories.Dtos;
using Restaurants.Application.Common;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<PagedResult<CategoryDto>>
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }

}
