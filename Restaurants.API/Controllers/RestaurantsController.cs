using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetCategoriesForRestaurant;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Application.Restaurants.Queries.GetRestaurantByName;
using Restaurants.Application.Restaurants.Queries.GetRestaurantStatistics;
using Restaurants.Application.Restaurants.Queries.GetTopRatedRestaurants;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll([FromQuery] GetAllRestaurantsQuery query)
        {
            var restaurants = await mediator.Send(query);
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDto?>> GetById([FromRoute] int id)
        {
            var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
            return Ok(restaurant);
        }

        [HttpGet("Name")]
        public async Task<ActionResult<RestaurantDto?>> GetByName([FromQuery] string name)
        {
            var restaurant = await mediator.Send(new GetRestaurantByNameQuery(name));
            return Ok(restaurant);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            await mediator.Send(new DeleteRestaurantCommand(id));
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, [FromBody] UpdateRestaurantCommand command)
        {
            command.Id = id;            // حقن الـ Id القادم من الـ route
            await mediator.Send(command);
            return NoContent();
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = UserRoles.Owner)]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command)
        {
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpGet("{restaurantId}/Categories")]
        public async Task<ActionResult<List<string>>> GetCategoriesForRestaurant([FromRoute] int restaurantId = 400)
        {
            var categories = await mediator.Send(new GetCategoriesForRestaurantQuery(restaurantId));
            return Ok(categories);
        }

        [HttpGet("TopRated")]
        public async Task<ActionResult<List<RestaurantDto>>> GetTopRated([FromQuery] int count = 5)
        {
            var result = await mediator.Send(new GetTopRatedRestaurantsQuery(count));
            return Ok(result);
        }

        [HttpGet("{restaurantId}/Statistics")]
        public async Task<ActionResult<RestaurantStatisticsDto>> GetStatistics([FromRoute] int restaurantId)
        {
            var result = await mediator.Send(new GetRestaurantStatisticsQuery(restaurantId));
            return Ok(result);
        }
    }
}
