using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Categories.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdHandler(
    ILogger<GetCategoryByIdHandler> logger,
    ICategoryRepository categoryRepository,
    IMapper mapper) : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Category with id : {CategoryID}", request.CategoryId);

            var category = await categoryRepository.GetByIdAsync(request.CategoryId)
                ?? throw new NotFoundException(nameof(Category), request.CategoryId.ToString());

            var result = mapper.Map<CategoryDto>(category);

            return result;
        }
    }

}
