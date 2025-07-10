using MediatR;
using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using System.Data;

namespace Restaurants.Application.User.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler(
        UserManager<ApplicationUser> userManager) : IRequestHandler<ResetPasswordCommand, string>
    {
        public async Task<string> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email)
                ?? throw new NotFoundException(nameof(ApplicationUser), request.Email.ToString());

            if (user.ResetCode != request.ResetCode)
                throw new BadRequestException("Invalid reset code");

            var passwordCheck = await userManager.CheckPasswordAsync(user, request.NewPassword);
            if (passwordCheck)
                throw new BadRequestException($"New password {request.NewPassword} be the same as the current password.");


            var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);

            var result = await userManager.ResetPasswordAsync(user, resetToken, request.NewPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new BadRequestException($"Failed to reset password: {errors}");
            }

            user.ResetCode = null;
            await userManager.UpdateAsync(user);

            return "Password has been reset successfully!";
        }
    }
}
