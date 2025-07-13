using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.User.Dtos;
using Restaurants.Application.User.Queries.GetAllUsers;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.User.Queries.GetUsersByRole
{
    public class GetUsersByRoleQueryHandler(ILogger<GetAllUsersQueryHandler> logger,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IMapper mapper) : IRequestHandler<GetUsersByRoleQuery, PagedResult<UserDto>>
    {
        public async Task<PagedResult<UserDto>> Handle(GetUsersByRoleQuery request, CancellationToken cancellationToken)
        {
            var roleExists = await roleManager.RoleExistsAsync(request.Role);
            if (!roleExists)
                throw new NotFoundException(nameof(IdentityRole), request.Role);
            //throw new BadRequestException("Role does not exist");

            var users = await userManager.GetUsersInRoleAsync(request.Role) ??
                throw new BadRequestException("Users does not exist");

            var usersDto = mapper.Map<IEnumerable<UserDto>>(users);


            foreach (var userDto in usersDto)
            {
                var user = users.FirstOrDefault(u => u.Id == userDto.Id)
                     ?? throw new NotFoundException(nameof(ApplicationUser), request.Role.ToString()); ;
                userDto.Roles = await userManager.GetRolesAsync(user!);
            }

            var result = new PagedResult<UserDto>(usersDto, usersDto.Count(), request.PageSize, request.PageNumber);
            return result;
        }
    }
}
