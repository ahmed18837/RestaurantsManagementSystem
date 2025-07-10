using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Interfaces.Services;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.User.Commands.Resend2FACode
{
    public class Resend2FACodeCommandHandler(ILogger<Resend2FACodeCommandHandler> logger,
        UserManager<ApplicationUser> userManager,
        ITokenService tokenService,
        IEmailService emailService) : IRequestHandler<Resend2FACodeCommand, string>
    {
        public async Task<string> Handle(Resend2FACodeCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email)
                ?? throw new NotFoundException(nameof(ApplicationUser), request.Email.ToString());

            if (user.TwoFactorSentAt != null && (DateTime.Now - user.TwoFactorSentAt.Value).TotalSeconds < 60)
            {
                throw new Exception("Please wait at least 1 minute before requesting a new code.");
            }

            if (user.LockoutEnd.HasValue && user.LockoutEnd > DateTime.Now)
            {
                throw new Exception($"Your account is locked. Try again at {user.LockoutEnd.Value.ToLocalTime()}.");
            }

            if (user.LastTwoFactorAttempt != null && (DateTime.Now - user.LastTwoFactorAttempt.Value).TotalHours >= 1)
            {
                user.TwoFactorAttempts = 0;
            }

            if (user.TwoFactorAttempts >= 5)
            {
                throw new Exception("You have exceeded the maximum number of attempts. Please try again later.");
            }

            var newCode = tokenService.GenerateCode();

            logger.LogInformation("Send a 2FAC {NewCode} To {Email}", newCode, request.Email);

            user.TwoFactorCode = newCode;
            user.TwoFactorCodeExpiration = DateTime.Now.AddMinutes(10);

            user.TwoFactorSentAt = DateTime.Now;

            user.TwoFactorAttempts += 1;
            user.LastTwoFactorAttempt = DateTime.Now;

            await userManager.UpdateAsync(user);

            var subject = "🔐 Your New Two-Factor Authentication Code";

            var message = $@"
<html>
    <body style='font-family: Arial, sans-serif; color: #333;'>
        <h2 style='color: #2e6c80;'>Two-Factor Authentication</h2>
        <p>Hello <strong>{user.FullName}</strong>,</p>
        <p>You requested a new 2FA code. Please use the code below:</p>
        <h1 style='color: #007BFF; font-size: 32px;'>{newCode}</h1>
        <p>This code will expire in <strong>10 minutes</strong>.</p>
        <p>If you did not request this, please ignore this email.</p>
        <br />
        <p style='font-size: 14px; color: #888;'>— Restaurants Security Team</p>
    </body>
</html>";


            await emailService.SendEmailAsync(user.Email, subject, message);

            return "A new 2FA code has been sent to your email.";
        }
    }
}
