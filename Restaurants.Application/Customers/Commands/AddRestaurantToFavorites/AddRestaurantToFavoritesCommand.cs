using MediatR;
using System.ComponentModel;

namespace Restaurants.Application.Customers.Commands.AddRestaurantToFavorites
{
    public class AddRestaurantToFavoritesCommand(int restaurantId) : IRequest
    {
        [DefaultValue(400)]
        public int RestaurantId { get; set; } = restaurantId;
    }
}
