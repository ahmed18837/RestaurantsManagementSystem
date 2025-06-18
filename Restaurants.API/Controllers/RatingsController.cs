using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Ratings.Dtos;
using Restaurants.Application.Ratings.Queries.GetAllRatings;
using Restaurants.Application.Ratings.Queries.GetRatingById;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController(IMediator mediator) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RatingDto>>> GetAll([FromQuery] GetAllRatingsQuery query)
        {
            var ratings = await mediator.Send(query);
            return Ok(ratings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RatingDto?>> GetById([FromRoute] int id)
        {
            var rating = await mediator.Send(new GetRatingByIdQuery(id));
            return Ok(rating);
        }
    }
}
