using MediatR;

namespace Restaurants.Application.User.Queries.GetRolesByEmail
{
    public class GetRolesByEmailQuery(string email) : IRequest<IEnumerable<string>>
    {
        public string Email { get; } = email;
    }
}
