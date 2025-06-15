using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Categories.Dtos;
using Restaurants.Application.Common;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesHandler(ILogger<GetAllCategoriesHandler> logger,
    ICategoryRepository categoryRepository,
    IMapper mapper) : IRequestHandler<GetAllCategoriesQuery, PagedResult<CategoryDto>>
    {
        async Task<PagedResult<CategoryDto>> IRequestHandler<GetAllCategoriesQuery, PagedResult<CategoryDto>>.Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving All Categories");

            var (categories, totalCount) = await categoryRepository.GetAllMatchingAsync(request.SearchPhrase,
               request.PageSize,
               request.PageNumber,
               request.SortBy,
               request.SortDirection);

            var categoriesDtos = mapper.Map<IEnumerable<CategoryDto>>(categories);

            var result = new PagedResult<CategoryDto>(categoriesDtos, totalCount, request.PageSize, request.PageNumber);
            return result;
        }
    }

}
