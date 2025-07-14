using Microsoft.Extensions.Logging;
using Restaurants.Application.User;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Services.Authorize
{
    public class DishAuthorizationService(
        ILogger<DishAuthorizationService> logger,
        IUserContext userContext) : IDishAuthorizationService
    {
        public bool Authorize(Restaurant restaurant, ResourceOperation operation)
        {
            var user = userContext.GetCurrentUser();
            if (user == null)
            {
                logger.LogWarning("User is not authenticated");
                return false;
            }

            logger.LogInformation(
                "Authorizing user {UserEmail} to {Operation} on restaurant {RestaurantName}",
                user?.Email, operation, restaurant.Name);

            if (operation == ResourceOperation.Read)
                return true;

            if (operation == ResourceOperation.Create &&
                (user!.IsInRole(UserRoles.Admin) || restaurant.OwnerId == user.Id))
                return true;

            logger.LogWarning("Authorization failed");
            return false;
        }

        public bool Authorize(Dish dish, ResourceOperation operation)
        {
            var user = userContext.GetCurrentUser();
            if (user == null)
            {
                logger.LogWarning("User is not authenticated");
                return false;
            }

            logger.LogInformation(
                "Authorizing user {UserEmail} to {Operation} on dish {DishId}",
                user?.Email, operation, dish.Id);

            if (operation == ResourceOperation.Read)
                return true;

            if (user!.IsInRole(UserRoles.Admin))
                return true;

            if ((operation == ResourceOperation.Update || operation == ResourceOperation.Delete) &&
               dish.Restaurant.OwnerId == user.Id)
                return true;

            logger.LogWarning("Authorization failed");
            return false;
        }
    }
}
