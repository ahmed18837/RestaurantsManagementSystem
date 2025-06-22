using MediatR;
using System.ComponentModel;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommand : IRequest<int>
    {
        [DefaultValue("Default Restaurant")]
        public string Name { get; set; } = default!;

        [DefaultValue("Default description")]
        public string Description { get; set; } = default!;

        [DefaultValue("Japanese")]
        public string Category { get; set; } = default!;

        [DefaultValue(true)]
        public bool HasDelivery { get; set; }

        [DefaultValue("info@example.com")]
        public string? ContactEmail { get; set; }

        [DefaultValue("+201234567890")]
        public string? ContactNumber { get; set; }

        [DefaultValue("Cairo")]
        public string? City { get; set; }

        [DefaultValue("Main Street 1")]
        public string? Street { get; set; }

        [DefaultValue("12-345")]
        public string? PostalCode { get; set; }
    }

}
