using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Interfaces
{
    public interface IDishAuthorizationService
    {
        bool Authorize(Restaurant restaurant, ResourceOperation operation);

        bool Authorize(Dish dish, ResourceOperation operation);
    }
}
