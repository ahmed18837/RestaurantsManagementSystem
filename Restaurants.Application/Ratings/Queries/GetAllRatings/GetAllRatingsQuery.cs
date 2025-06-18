using MediatR;
using Restaurants.Application.Common;
using Restaurants.Application.Ratings.Dtos;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Ratings.Queries.GetAllRatings
{
    public class GetAllRatingsQuery : IRequest<PagedResult<RatingDto>>
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
