using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler(ILogger<DeleteCategoryCommandHandler> logger,
    ICategoryAuthorizationService categoryAuthorizationService,
    ICategoriesRepository categoriesRepository) : IRequestHandler<DeleteCategoryCommand>
    {
        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await categoriesRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Category), request.Id.ToString());

            if (!categoryAuthorizationService.CanModifyCategory(category))
                throw new ForbidException();

            logger.LogInformation("Deleting Category with id: {CategoryId}", request.Id);

            await categoriesRepository.DeleteAsync(category);
        }
    }

}
