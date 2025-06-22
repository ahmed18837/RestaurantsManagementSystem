using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Ratings.Commands.UpdateRating
{
    public class UpdateRatingCommandHandler(ILogger<UpdateRatingCommandHandler> logger,
    IRatingsRepository ratingsRepository,
    IMapper mapper) : IRequestHandler<UpdateRatingCommand>
    {
        public async Task Handle(UpdateRatingCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating rating with id: {RatingId} with {@UpdatedRating}", request.Id, request);

            var rating = await ratingsRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Rating), request.Id.ToString());

            mapper.Map(request, rating);

            await ratingsRepository.SaveChanges();
        }
    }

}
