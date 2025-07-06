using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommand : IRequest<int>
    {
        [JsonIgnore]
        public int RestaurantId { get; set; }

        [DefaultValue("Latte")]
        public string Name { get; set; } = default!;

        [DefaultValue("Write Description")]
        public string? Description { get; set; }

        [DefaultValue(20.5)]
        public decimal Price { get; set; }

        public IFormFile? Image { get; set; }

        [DefaultValue("Pasta")]
        public string CategoryName { get; set; } = default!;
    }

}
