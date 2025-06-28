using MediatR;
using System.ComponentModel;

namespace Restaurants.Application.Customers.Commands.AddRestaurantToFavorites
{
    public class AddRestaurantToFavoritesCommand(int customerId, int restaurantId) : IRequest
    {
        [DefaultValue(500)]
        public int CustomerId { get; set; } = customerId;

        [DefaultValue(400)]
        public int RestaurantId { get; set; } = restaurantId;
    }
}
