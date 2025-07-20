using MediatR;
using Microsoft.AspNetCore.Identity;
using Restaurants.Application.User.Commands.RegisterUser;

namespace Restaurants.Application.User.Commands.RegisterMultipleUsers
{
    public class RegisterMultipleUsersCommand : IRequest<List<IdentityResult>>
    {
        public List<RegisterUserCommand> Users { get; set; } = [];
    }

}
