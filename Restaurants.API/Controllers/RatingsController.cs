using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Ratings.Commands.CreateRating;
using Restaurants.Application.Ratings.Commands.DeleteRating;
using Restaurants.Application.Ratings.Commands.UpdateRating;
using Restaurants.Application.Ratings.Dtos;
using Restaurants.Application.Ratings.Queries.GetAllRatings;
using Restaurants.Application.Ratings.Queries.GetRatingById;

namespace Restaurants.API.Controllers
{
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRating([FromRoute] int id)
        {
            await mediator.Send(new DeleteRatingCommand(id));
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRating([FromRoute] int id, [FromBody] UpdateRatingCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = UserRoles.Owner)]
        public async Task<IActionResult> CreateRating([FromBody] CreateRatingCommand command)
        {
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }
    }
}
