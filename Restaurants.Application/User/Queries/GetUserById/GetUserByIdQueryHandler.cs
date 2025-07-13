using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurants.Application.User.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.User.Queries.GetUserById
{
    public class GetUserByIdQueryHandler(ILogger<GetUserByIdQueryHandler> logger,
     UserManager<ApplicationUser> userManager,
     IMapper mapper) : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting user {UserId}", request.Id.ToString());

            var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken: cancellationToken)
                    ?? throw new NotFoundException(nameof(ApplicationUser), request.Id.ToString());

            var userDto = mapper.Map<UserDto>(user);
            userDto.Roles = await userManager.GetRolesAsync(user);

            return userDto;
        }
    }
}
