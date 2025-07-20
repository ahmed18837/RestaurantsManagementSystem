using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Categories.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler(
    ILogger<GetCategoryByIdQueryHandler> logger,
    ICategoriesRepository categoriesRepository,
    IMapper mapper) : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await categoriesRepository.GetByIdWithDishesAndRestaurantsAsync(request.CategoryId)
                ?? throw new NotFoundException(nameof(Category), request.CategoryId.ToString());

            var result = mapper.Map<CategoryDto>(category);

            logger.LogInformation("Category with id : {CategoryID}", request.CategoryId);

            return result;
        }
    }

}
