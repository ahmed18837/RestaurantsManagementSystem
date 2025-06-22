using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Categories.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Categories.Queries.GetCategoryByName
{
    public class GetCategoryByNameQueryHandler(ILogger<GetCategoryByNameQueryHandler> logger,
     ICategoriesRepository categoriesRepository,
     IMapper mapper) : IRequestHandler<GetCategoryByNameQuery, CategoryDto>
    {
        public async Task<CategoryDto> Handle(GetCategoryByNameQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting Category {CategoryName}", request.Name);

            var category = await categoriesRepository.GetByNameAsync(request.Name)
                    ?? throw new NotFoundNameException(nameof(Category), request.Name);

            var categoryDto = mapper.Map<CategoryDto>(category);

            return categoryDto;
        }
    }
}
