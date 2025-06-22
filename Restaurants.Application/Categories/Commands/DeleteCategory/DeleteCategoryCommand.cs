using MediatR;

namespace Restaurants.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand(int id) : IRequest
    {
        public int Id { get; } = id;
    }

}
