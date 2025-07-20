using Microsoft.Extensions.Logging;
using Restaurants.Application.User;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Services.Authorize;


public class CategoryAuthorizationService(
        ILogger<CategoryAuthorizationService> logger,
        IUserContext userContext) : ICategoryAuthorizationService
{
    public bool CanModifyCategory(Category category)
    {
        var user = userContext.GetCurrentUser();
        if (user is null)
        {
            logger.LogWarning("Unauthorized user attempted to access Category {CategoryId}", category.Id);
            return false;
        }

        if (user.IsInRole(UserRoles.Admin) || user.IsInRole(UserRoles.SuperAdmin))
            return true;

        var ownerId = user.Id;

        bool isOwnerOfCategory = category.Dishes
            .Any(d => d.Restaurant != null && d.Restaurant.OwnerId == ownerId);

        if (!isOwnerOfCategory)
        {
            logger.LogWarning("User {UserId} tried to access Category {CategoryId} without ownership.", user.Id, category.Id);
            return false;
        }

        return true;
    }
}
