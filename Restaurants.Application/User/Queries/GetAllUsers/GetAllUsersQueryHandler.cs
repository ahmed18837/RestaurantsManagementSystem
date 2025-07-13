using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.User.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.User.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler(ILogger<GetAllUsersQueryHandler> logger,
        UserManager<ApplicationUser> userManager,
        IMapper mapper) : IRequestHandler<GetAllUsersQuery, PagedResult<UserDto>>
    {
        public async Task<PagedResult<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Get All Users");

            var users = userManager.Users.ToList();

            if (users == null || users.Count == 0)
                throw new BadRequestException("Users Not Found!");

            var usersDto = mapper.Map<List<UserDto>>(users);

            foreach (var userDto in usersDto)
            {
                var user = users.FirstOrDefault(u => u.Id == userDto.Id)
                    ?? throw new NotFoundException(nameof(ApplicationUser), userDto.ToString()!);
                userDto.Roles = await userManager.GetRolesAsync(user);
            }

            var result = new PagedResult<UserDto>(usersDto, usersDto.Count, request.PageSize, request.PageNumber);
            return result;
        }
    }
}
