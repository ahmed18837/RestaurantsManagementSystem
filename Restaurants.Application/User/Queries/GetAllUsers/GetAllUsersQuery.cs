using MediatR;
using Restaurants.Application.Common;
using Restaurants.Application.User.Dtos;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.User.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<PagedResult<UserDto>>
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
