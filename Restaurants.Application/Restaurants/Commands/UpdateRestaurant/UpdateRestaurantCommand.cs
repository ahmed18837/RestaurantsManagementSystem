using MediatR;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommand : IRequest
    {
        [JsonIgnore]
        public int Id { get; set; }

        [DefaultValue("Default Restaurant")]
        public string Name { get; set; } = default!;

        [DefaultValue("Default description")]
        public string Description { get; set; } = default!;

        [DefaultValue("Japanese")]
        public string Category { get; set; } = default!;

        [DefaultValue(true)]
        public bool HasDelivery { get; set; }
    }
}
