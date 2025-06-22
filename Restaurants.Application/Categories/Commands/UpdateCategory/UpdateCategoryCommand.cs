using MediatR;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Restaurants.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest
    {
        [JsonIgnore]
        public int Id { get; set; }

        [DefaultValue("Breakfast")]
        public string Name { get; set; } = default!;

        [DefaultValue("Write Description")]
        public string? Description { get; set; }
    }
}
