using MediatR;

namespace Restaurants.Application.Ratings.Commands.DeleteRating
{
    public class DeleteRatingCommand(int id) : IRequest
    {
        public int Id { get; } = id;
    }

}
