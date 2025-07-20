using Microsoft.Extensions.Logging;
using Restaurants.Application.User;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Services.Authorize
{
    public class RatingAuthorizationService(
        ILogger<RatingAuthorizationService> logger,
        IUserContext userContext) : IRatingAuthorizationService
    {
        public bool Authorize(Rating rating, ResourceOperation op)
        {
            var user = userContext.GetCurrentUser();
            if (user is null)
                return false;

            logger.LogInformation("User {User} tries {Op} on rating {Id}",
                                  user.Email, op, rating.Id);

            if (op == ResourceOperation.Read || op == ResourceOperation.Create)
                return true;

            if (op is ResourceOperation.Delete or ResourceOperation.Update)
            {
                if (user.IsInRole(UserRoles.Admin) || user.IsInRole(UserRoles.SuperAdmin))
                    return true;

                if (rating.Customer?.ApplicationUserId == user.Id)
                    return true;

                if (rating.Dish.Restaurant.OwnerId == user.Id)
                    return true;
            }

            logger.LogWarning("Authorization failed");
            return false;
        }
    }
}
