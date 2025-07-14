using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Interfaces
{
    public interface IRatingAuthorizationService
    {
        bool Authorize(Rating rating, ResourceOperation operation);
    }
}
