using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurants.Application.User.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.User.Queries.GetUserByName
{
    public class GetUserByNameQueryHandler(ILogger<GetUserByNameQueryHandler> logger,
     UserManager<ApplicationUser> userManager,
     IMapper mapper) : IRequestHandler<GetUserByNameQuery, UserDto>
    {
        public async Task<UserDto> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting User {UserName}", request.FullName);

            var user = await userManager.Users.FirstOrDefaultAsync(u => u.FullName.Contains(request.FullName), cancellationToken: cancellationToken)
                    ?? throw new NotFoundNameException(nameof(ApplicationUser), request.FullName);

            var userDto = mapper.Map<UserDto>(user);

            return userDto;
        }
    }
}
