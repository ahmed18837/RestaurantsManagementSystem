using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Interfaces
{
    public interface ICategoryAuthorizationService
    {
        bool CanModifyCategory(Category category);
    }
}


