using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.User.Commands.ForgetPassword;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.User.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler(ILogger<ForgetPasswordCommandHandler> logger,
         UserManager<ApplicationUser> userManager) : IRequestHandler<ChangePasswordCommand, string>
    {
        public async Task<string> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Change Old Password To New Password  {NewPassword} To {Email}", request.NewPassword, request.Email);

            var user = await userManager.FindByEmailAsync(request.Email)
            ?? throw new NotFoundException(nameof(ApplicationUser), request.Email);

            var isOldPasswordCorrect = await userManager.CheckPasswordAsync(user, request.CurrentPassword);
            if (!isOldPasswordCorrect)
                throw new BadRequestException("Incorrect Current password");

            if (request.CurrentPassword == request.NewPassword)
                throw new BadRequestException("New password cannot be the same as the Current password");

            var result = await userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new BadRequestException($"Password change failed: {errors}");
            }

            return "Password changed successfully";
        }
    }
}
