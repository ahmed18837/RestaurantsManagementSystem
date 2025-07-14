using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.User.Commands.DeleteUser
{
    internal class DeleteUserCommandHandler(ILogger<DeleteUserCommandHandler> logger,
        UserManager<ApplicationUser> userManager) : IRequestHandler<DeleteUserCommand>
    {
        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Id))
                throw new ArgumentException("Id is required.");

            var user = await userManager.FindByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(ApplicationUser), request.Id.ToString());

            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
                throw new BadRequestException("Failed to delete user.");
            logger.LogInformation("Delete a User with email : {Email}", user.Email);
        }
    }
}
