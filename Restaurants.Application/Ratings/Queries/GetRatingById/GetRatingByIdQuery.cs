using MediatR;
using Restaurants.Application.Ratings.Dtos;

namespace Restaurants.Application.Ratings.Queries.GetRatingById
{
    public class GetRatingByIdQuery(int id) : IRequest<RatingDto>
    {
        public int Id { get; } = id;
    }
}
