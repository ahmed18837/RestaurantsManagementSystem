using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Interfaces.Services;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.User.Commands.Send2FACode
{
    public class Send2FACodeCommandHandler(ILogger<Send2FACodeCommandHandler> logger,
        UserManager<ApplicationUser> userManager,
        ITokenService tokenService,
        IEmailService emailService) : IRequestHandler<Send2FACodeCommand, string>
    {
        public async Task<string> Handle(Send2FACodeCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email)
                ?? throw new NotFoundException(nameof(ApplicationUser), request.Email.ToString());

            if (user.TwoFactorCodeExpiration != null && user.TwoFactorCodeExpiration > DateTime.Now)
                throw new BadRequestException("A valid 2FA code has already been sent. Please check your email.");

            var twoFactorCode = tokenService.GenerateCode();

            logger.LogInformation("Send a 2FAC {TwoFactorCode} To {Email}", twoFactorCode, request.Email);

            user.TwoFactorCode = twoFactorCode;
            user.TwoFactorCodeExpiration = DateTime.Now.AddMinutes(10);

            await userManager.UpdateAsync(user);

            var expirationTime = DateTime.Now.AddMinutes(10);
            user.TwoFactorCode = twoFactorCode;
            user.TwoFactorCodeExpiration = expirationTime;

            await userManager.UpdateAsync(user);

            var subject = "🔐 Your Two-Factor Authentication Code";

            var message = $@"
<html>
    <body style='font-family: Arial, sans-serif; color: #333;'>
        <h2 style='color: #2e6c80;'>Two-Factor Authentication</h2>
        <p>Hello <strong>{user.FullName}</strong>,</p>
        <p>Your 2FA code is:</p>
        <h1 style='color: #007BFF; font-size: 32px;'>{twoFactorCode}</h1>
        <p>This code is valid until <strong>{expirationTime:HH:mm:ss}</strong> ({expirationTime:dddd, MMMM dd, yyyy}).</p>
        <p>If you didn’t request this, please ignore this email.</p>
        <br />
        <p style='font-size: 14px; color: #888;'>— Restaurants Security Team</p>
    </body>
</html>";


            await emailService.SendEmailAsync(user.Email, subject, message);

            return "A 2FA code has been sent to your email.";
        }
    }
}
