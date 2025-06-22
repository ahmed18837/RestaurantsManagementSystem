using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using System.Data;

namespace Restaurants.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler(ILogger<CreateCategoryCommandHandler> logger,
        IMapper mapper,
        ICategoriesRepository categoriesRepository) : IRequestHandler<CreateCategoryCommand, int>
    {
        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("creating a new Category {@Category}", request);

            var existCategory = await categoriesRepository.GetByNameAsync(request.Name);
            if (existCategory != null)
                throw new DuplicateNameException("This Category already exists"); // 409

            var category = mapper.Map<Category>(request);

            await categoriesRepository.AddAsync(category);

            return category.Id;
        }
    }

}
