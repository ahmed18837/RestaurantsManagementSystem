using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Interfaces
{
    public interface ICustomerAuthorizationService
    {
        bool Authorize(Customer customer, ResourceOperation operation);
        bool CanAddToFavorites(Customer customer);
    }
}


