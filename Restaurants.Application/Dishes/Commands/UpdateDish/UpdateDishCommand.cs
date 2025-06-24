using MediatR;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Restaurants.Application.Dishes.Commands.UpdateDish
{
    public class UpdateDishCommand : IRequest
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public int RestaurantId { get; set; }

        [DefaultValue("Breakfast")]
        public string Name { get; set; } = default!;

        [DefaultValue("Write Description")]
        public string? Description { get; set; }
    }
}
