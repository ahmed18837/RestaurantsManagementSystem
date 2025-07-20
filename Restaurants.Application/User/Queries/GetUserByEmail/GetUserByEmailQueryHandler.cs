using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.User.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.User.Queries.GetUserByEmail
{
    public class GetUserByEmailQueryHandler(ILogger<GetUserByEmailQueryHandler> logger,
     UserManager<ApplicationUser> userManager,
     IMapper mapper) : IRequestHandler<GetUserByEmailQuery, UserDto>
    {
        public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email)
                 ?? throw new NotFoundException(nameof(ApplicationUser), request.Email);

            var userDto = mapper.Map<UserDto>(user);
            userDto.Roles = await userManager.GetRolesAsync(user);

            logger.LogInformation("Get User By Email : {Email}", request.Email);

            return userDto;
        }
    }
}
