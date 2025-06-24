using MediatR;

namespace Restaurants.Application.Dishes.Commands.DeleteDish
{
    public class DeleteDishCommand(int RestaurantId, int id) : IRequest
    {
        public int RestaurantId { get; } = RestaurantId;
        public int Id { get; } = id;
    }
}
