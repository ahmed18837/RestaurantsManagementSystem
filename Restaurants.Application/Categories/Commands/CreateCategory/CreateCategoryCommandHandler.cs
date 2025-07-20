using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using System.Data;

namespace Restaurants.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler(ILogger<CreateCategoryCommandHandler> logger,
        IMapper mapper,
        ICategoryAuthorizationService categoryAuthorizationService,
        ICategoriesRepository categoriesRepository) : IRequestHandler<CreateCategoryCommand, int>
    {
        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var existCategory = await categoriesRepository.GetByNameAsync(request.Name);

            if (!categoryAuthorizationService.CanModifyCategory(existCategory!))
                throw new ForbidException();

            if (existCategory != null)
                throw new DuplicateNameException("This Category already exists"); // 409

            var category = mapper.Map<Category>(request);

            await categoriesRepository.AddAsync(category);

            logger.LogInformation("creating a new Category {@Category}", request);

            return category.Id;
        }
    }

}
