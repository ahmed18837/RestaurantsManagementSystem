using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Common;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDish;
using Restaurants.Application.Dishes.Commands.UpdateDish;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Queries.GetDishByCategoryIdForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishByName;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;
using Restaurants.Domain.Constants;

namespace Restaurants.API.Controllers
{
    [Route("api/restaurants/{restaurantId:int}/[controller]")]
    [ApiController]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PagedResult<DishDto>>> GetAllForRestaurant(
        [FromRoute] int restaurantId,
        [FromQuery] string? searchPhrase = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 5,
        [FromQuery] string? sortBy = null,
        [FromQuery] SortDirection sortDirection = SortDirection.Ascending)
        {
            var query = new GetDishesForRestaurantQuery(restaurantId)
            {
                SearchPhrase = searchPhrase,
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortBy = sortBy,
                SortDirection = sortDirection
            };

            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<DishDto>> GetByIdForRestaurant([FromRoute] int restaurantId, [FromRoute] int Id)
        {
            var dish = await mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId, Id));
            return Ok(dish);
        }

        [HttpGet("Name")]
        public async Task<ActionResult<DishDto?>> GetByName([FromRoute] int restaurantId, [FromQuery] string name)
        {
            var dish = await mediator.Send(new GetDishByNameQuery(restaurantId, name));
            return Ok(dish);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDish([FromRoute] int restaurantId, [FromRoute] int id)
        {
            await mediator.Send(new DeleteDishCommand(restaurantId, id));
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateDish([FromRoute] int restaurantId, [FromRoute] int id, [FromBody] UpdateDishCommand command)
        {
            command.Id = id;
            command.RestaurantId = restaurantId;

            await mediator.Send(command);
            return NoContent();
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = UserRoles.Owner)]
        public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, [FromForm] CreateDishCommand command)
        {
            command.RestaurantId = restaurantId;
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetByIdForRestaurant), new { restaurantId, Id = id }, null);
        }

        [HttpGet("CategoryId")]
        public async Task<ActionResult<DishDto>> GetByCategoryIdForRestaurant([FromRoute] int restaurantId, [FromQuery] int categoryId,
        [FromQuery] string? searchPhrase = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 5,
        [FromQuery] string? sortBy = null,
        [FromQuery] SortDirection sortDirection = SortDirection.Ascending)
        {
            var query = new GetDishesByCategoryIdForRestaurantQuery(restaurantId, categoryId)
            {
                SearchPhrase = searchPhrase,
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortBy = sortBy,
                SortDirection = sortDirection
            };

            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
