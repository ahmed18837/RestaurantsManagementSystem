using MediatR;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Restaurants.Application.Ratings.Commands.UpdateRating
{
    public class UpdateRatingCommand : IRequest
    {
        [JsonIgnore]
        public int Id { get; set; }

        [DefaultValue(3)]
        public int Stars { get; set; }
    }
}
