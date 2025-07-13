using MediatR;

namespace Restaurants.Application.User.Queries.GetAllRoles
{
    public class GetAllRolesQuery : IRequest<IEnumerable<string>>
    {
    }
}
