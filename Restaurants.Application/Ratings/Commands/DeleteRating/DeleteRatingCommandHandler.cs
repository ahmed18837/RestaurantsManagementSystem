using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Ratings.Commands.DeleteRating
{
    public class DeleteRatingCommandHandler(ILogger<DeleteRatingCommandHandler> logger,
    IRatingsRepository ratingsRepository,
    IRatingAuthorizationService ratingAuthorizationService) : IRequestHandler<DeleteRatingCommand>
    {
        public async Task Handle(DeleteRatingCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting rating with id: {RatingId}", request.Id);

            var rating = await ratingsRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Rating), request.Id.ToString());

            if (!ratingAuthorizationService.Authorize(rating, ResourceOperation.Delete))
                throw new ForbidException();

            await ratingsRepository.DeleteAsync(rating);
        }
    }

}
