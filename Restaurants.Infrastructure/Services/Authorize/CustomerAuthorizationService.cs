using Microsoft.Extensions.Logging;
using Restaurants.Application.User;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Services.Authorize;

public class CustomerAuthorizationService(
    ILogger<CustomerAuthorizationService> logger,
    IUserContext userContext) : ICustomerAuthorizationService
{
    public bool Authorize(Customer customer, ResourceOperation op)
    {
        var user = userContext.GetCurrentUser();
        if (user is null)
        {
            logger.LogWarning("Anonymous user attempted {Op} on customer {CustomerId}", op, customer.Id);
            return false;
        }

        // Admin يمتلك كل الصلاحيات
        if (user.IsInRole(UserRoles.Admin))
            return true;

        // القراءة مسموحة لصاحب الحساب
        if (op == ResourceOperation.Read && (customer.ApplicationUserId == user.Id) || user.CustomerId == customer.Id)
            return true;

        // ❌ Update / Delete لغير الـ Admin --> مرفوض
        logger.LogWarning("Authorization failed ({Op}) by {User}", op, user.Email);
        return false;
    }

    public bool CanAddToFavorites(Customer customer)
    {
        var user = userContext.GetCurrentUser();
        if (user is null) return false;

        // Admin أو صاحب الحساب
        return user.IsInRole(UserRoles.Admin) || user.CustomerId == customer.Id || customer.ApplicationUserId == user.Id;
    }
}
