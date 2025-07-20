using Microsoft.Extensions.Logging;
using Restaurants.Application.User;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Services.Authorize;

public class OrderAuthorizationService(
    ILogger<OrderAuthorizationService> logger,
    IUserContext userContext) : IOrderAuthorizationService
{
    public bool CanViewCustomerOrder(string customerApplicationUserId)
    {
        var user = userContext.GetCurrentUser();

        if (user is null)
        {
            logger.LogWarning("Unauthenticated user tried to access a customer's orders.");
            return false;
        }

        return user.Id == customerApplicationUserId ||
            user.IsInRole(UserRoles.Admin) ||
            user.IsInRole(UserRoles.SuperAdmin);
    }

    public bool CanViewAllOrders()
    {
        var user = userContext.GetCurrentUser();

        if (user is null)
        {
            logger.LogWarning("Unauthenticated user tried to access a customer's orders.");
            return false;
        }
        return user.IsInRole(UserRoles.Admin) || user.IsInRole(UserRoles.SuperAdmin);
    }

    public bool CanViewCustomerOrder(Customer customer)
    {
        var user = userContext.GetCurrentUser();

        if (user is null)
        {
            logger.LogWarning("Unauthenticated user tried to access a customer's orders.");
            return false;
        }
        return user.Id == customer.ApplicationUserId ||
               user.IsInRole(UserRoles.Admin) ||
               user.IsInRole(UserRoles.SuperAdmin);
    }

    public bool CanViewRestaurantOrders(string ownerId)
    {
        var user = userContext.GetCurrentUser();
        if (user is null)
        {
            logger.LogWarning("Unauthenticated access to restaurant orders for Owner {OwnerId}", ownerId);
            return false;
        }

        if (user.IsInRole(UserRoles.Admin) || user.IsInRole(UserRoles.SuperAdmin))
        {
            return true;
        }

        if (user.Id != ownerId)
        {
            logger.LogWarning("User {UserId} is not authorized to view orders for Owner {OwnerId}", user.Id, ownerId);
            return false;
        }

        return true;
    }


    public bool CanModifyOrder(Order order)
    {
        var user = userContext.GetCurrentUser();
        if (user is null)
        {
            logger.LogWarning("Unauthenticated user attempted to modify Order {OrderId}", order.Id);
            return false;
        }

        if (user.IsInRole(UserRoles.Admin) || user.IsInRole(UserRoles.SuperAdmin))
        {
            return true;
        }

        var userId = user.Id;

        // Allow if the user is the customer
        if (order.Customer?.ApplicationUserId == userId)
        {
            return true;
        }

        // Or if the user is owner of any restaurant involved in the order
        var isOwner = order.OrderItems.Any(item =>
            item.Dish?.Restaurant?.OwnerId == userId);

        if (!isOwner)
        {
            logger.LogWarning("User {UserId} attempted to modify Order {OrderId} without permission", user.Id, order.Id);
            return false;
        }

        return true;
    }
}
