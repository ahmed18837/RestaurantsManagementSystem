using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Interfaces.Services;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.User.Commands.ForgetPassword
{
    public class ForgetPasswordCommandHandler(ILogger<ForgetPasswordCommandHandler> logger,
         UserManager<ApplicationUser> userManager,
        ITokenService tokenService,
        IEmailService emailService) : IRequestHandler<ForgetPasswordCommand, string>
    {
        public async Task<string> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email)
                ?? throw new NotFoundException(nameof(ApplicationUser), request.Email);

            var resetCode = tokenService.GenerateCode();

            logger.LogInformation("Send a Reset Code {ResetCode} To {Email}", resetCode, request.Email);

            user.ResetCode = resetCode;

            await userManager.UpdateAsync(user);

            var subject = "🔐 Password Reset Code";

            var message = $@"
    <html>
    <body style='font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;'>
        <div style='max-width: 600px; margin: auto; background-color: #ffffff; border-radius: 8px; padding: 30px; box-shadow: 0 0 10px rgba(0,0,0,0.1);'>
            <h2 style='color: #007bff;'>Password Reset Request</h2>
            <p>Hello <strong>{user.FullName}</strong>,</p>
            <p>You requested to reset your password. Please use the following code to proceed:</p>
            <div style='background-color: #f1f1f1; padding: 15px; border-radius: 5px; text-align: center; font-size: 24px; font-weight: bold; color: #333;'>
                {resetCode}
            </div>
            <p style='margin-top: 20px;'>If you didn’t request this, please ignore this email.</p>
            <p>Thanks,<br/>The Restaurants Team</p>
        </div>
    </body>
    </html>";

            await emailService.SendEmailAsync(user.Email, subject, message);


            return "A password reset code has been sent to your email.";
        }
    }
}
