using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.User.Commands.Verify2FACode
{
    public class Verify2FACodeCommandHandler(ILogger<Verify2FACodeCommandHandler> logger,
        UserManager<ApplicationUser> userManager) : IRequestHandler<Verify2FACodeCommand, string>
    {
        public async Task<string> Handle(Verify2FACodeCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email)
             ?? throw new NotFoundException(nameof(ApplicationUser), request.Email);

            if (user.TwoFactorCode == null || user.TwoFactorCodeExpiration < DateTime.UtcNow)
                throw new Exception("The 2FA code has expired. Please request a new one.");

            if (user.TwoFactorCode != request.Code)
            {
                user.FailedTwoFactorAttempts++;

                if (user.FailedTwoFactorAttempts >= 5)
                {
                    user.LockoutEnd = DateTime.Now.AddMinutes(10);
                    await userManager.UpdateAsync(user);
                    throw new Exception("Too many failed attempts. Your account is locked for 15 minutes.");
                }

                await userManager.UpdateAsync(user);

                throw new Exception("Invalid 2FA code.");
            }

            user.FailedTwoFactorAttempts = 0;
            user.TwoFactorAttempts = 0;
            user.LockoutEnd = null;
            user.TwoFactorCode = null;
            user.TwoFactorCodeExpiration = null;

            await userManager.UpdateAsync(user);

            return "2FA verification successful";
        }
    }
}
