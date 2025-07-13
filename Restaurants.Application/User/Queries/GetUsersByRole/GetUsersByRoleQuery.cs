using MediatR;
using Restaurants.Application.Common;
using Restaurants.Application.User.Dtos;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.User.Queries.GetUsersByRole
{
    public class GetUsersByRoleQuery(string role) : IRequest<PagedResult<UserDto>>
    {
        public string Role { get; } = role;
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
