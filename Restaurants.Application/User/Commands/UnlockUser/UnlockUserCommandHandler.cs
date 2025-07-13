using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Interfaces.Services;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.User.Commands.UnlockUser
{
    public class UnlockUserCommandHandler(ILogger<UnlockUserCommandHandler> logger,
         UserManager<ApplicationUser> userManager,
        IEmailService emailService) : IRequestHandler<UnlockUserCommand, string>
    {
        public async Task<string> Handle(UnlockUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email)
                ?? throw new NotFoundException(nameof(ApplicationUser), request.Email);

            logger.LogInformation("Unlock User with Email : {Email}", request.Email);

            if (user.LockoutEnd.HasValue && user.LockoutEnd > DateTime.Now)
            {
                user.LockoutEnd = null;
                user.FailedTwoFactorAttempts = 0;
                await userManager.UpdateAsync(user);
                var subject = "✅ Your Restaurants Account Has Been Unlocked";

                var htmlBody = $@"
<!DOCTYPE html>
<html>
<body style='font-family: Arial, sans-serif; color:#333'>
  <div style='max-width:600px;margin:auto;padding:24px;border:1px solid #e2e2e2;border-radius:8px'>
    <h2 style='color:#28a745;'>Account Unlocked</h2>
    <p>Hello <strong>{user.FullName}</strong>,</p>
    <p>We’re pleased to inform you that your Restaurants account has been
       <span style='color:#28a745;font-weight:bold;'>unlocked</span>. You can sign in again at any time.</p>
    <p>If you experience further issues, please contact our support team.</p>
    <br/>
    <p style='font-size:14px;color:#888'>— Restaurants Security Team</p>
  </div>
</body>
</html>";

                await emailService.SendEmailAsync(user.Email!, subject, htmlBody);

                return "User account has been unlocked successfully and an email notification has been sent.";
            }

            return "User account is not locked.";
        }
    }
}
