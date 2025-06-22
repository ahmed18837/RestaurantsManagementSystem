using MediatR;
using System.ComponentModel;

namespace Restaurants.Application.Ratings.Commands.CreateRating
{
    public class CreateRatingCommand : IRequest<int>
    {
        [DefaultValue(3)]
        public int Stars { get; set; }

        [DefaultValue("Write a Comment")]
        public string? Comment { get; set; }

        [DefaultValue("Ahmed Ali")]
        public string CustomerName { get; set; } = default!;

        [DefaultValue("Garlic Bread")]
        public string DishName { get; set; } = default!;

        [DefaultValue("Pizza House")]
        public string RestaurantName { get; set; } = default!;
    }

}
