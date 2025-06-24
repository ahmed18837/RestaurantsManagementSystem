using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Ratings.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Ratings.Queries.GetRatingById
{
    public class GetRatingByIdQueryHandler(ILogger<GetRatingByIdQueryHandler> logger,
     IRatingsRepository ratingsRepository,
     IMapper mapper) : IRequestHandler<GetRatingByIdQuery, RatingDto>
    {
        public async Task<RatingDto> Handle(GetRatingByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting rating {RatingId}", request.Id);

            var rating = await ratingsRepository.GetByIdWithIncluded(request.Id)
                    ?? throw new NotFoundException(nameof(Rating), request.Id.ToString());

            var ratingDto = mapper.Map<RatingDto>(rating);

            //restaurantDto.LogoSasUrl = blobStorageService.GetBlobSasUrl(restaurant.LogoUrl);

            return ratingDto;
        }
    }
}
