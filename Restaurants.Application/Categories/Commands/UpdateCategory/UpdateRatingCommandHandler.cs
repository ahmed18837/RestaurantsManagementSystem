using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler(ILogger<UpdateCategoryCommandHandler> logger,
    ICategoriesRepository categoriesRepository,
    IMapper mapper) : IRequestHandler<UpdateCategoryCommand>
    {
        public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating Category with id: {CategoryId} with {@UpdatedCategory}", request.Id, request);

            var category = await categoriesRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Category), request.Id.ToString());

            mapper.Map(request, category);

            await categoriesRepository.SaveChanges();
        }
    }

}
