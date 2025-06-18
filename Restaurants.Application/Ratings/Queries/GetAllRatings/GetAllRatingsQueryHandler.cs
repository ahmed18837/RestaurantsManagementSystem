using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Ratings.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Ratings.Queries.GetAllRatings
{
    public class GetAllRatingsQueryHandler(ILogger<GetAllRatingsQueryHandler> logger,
     IMapper mapper,
     IRatingsRepository ratingsRepository) : IRequestHandler<GetAllRatingsQuery, PagedResult<RatingDto>>
    {
        public async Task<PagedResult<RatingDto>> Handle(GetAllRatingsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all customers");
            var (ratings, totalCount) = await ratingsRepository.GetAllMatchingAsync(request.SearchPhrase,
                request.PageSize,
                request.PageNumber,
                request.SortBy,
                request.SortDirection);

            var ratingDtos = mapper.Map<IEnumerable<RatingDto>>(ratings);

            var result = new PagedResult<RatingDto>(ratingDtos, totalCount, request.PageSize, request.PageNumber);
            return result;
        }
    }
}
