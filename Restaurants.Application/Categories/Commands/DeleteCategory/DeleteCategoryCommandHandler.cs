using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler(ILogger<DeleteCategoryCommandHandler> logger,
    ICategoriesRepository categoriesRepository) : IRequestHandler<DeleteCategoryCommand>
    {
        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting Category with id: {CategoryId}", request.Id);

            var rating = await categoriesRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Category), request.Id.ToString());

            await categoriesRepository.DeleteAsync(rating);
        }
    }

}
