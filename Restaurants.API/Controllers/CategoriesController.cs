using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Categories.Commands.CreateCategory;
using Restaurants.Application.Categories.Commands.DeleteCategory;
using Restaurants.Application.Categories.Commands.UpdateCategory;
using Restaurants.Application.Categories.Dtos;
using Restaurants.Application.Categories.Queries.GetAllCategories;
using Restaurants.Application.Categories.Queries.GetCategoryById;
using Restaurants.Application.Categories.Queries.GetCategoryByName;

namespace Restaurants.API.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CategoriesController(IMediator mediator) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll([FromQuery] GetAllCategoriesQuery query)
        {
            var categories = await mediator.Send(query);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto?>> GetById([FromRoute] int id)
        {
            var category = await mediator.Send(new GetCategoryByIdQuery(id));
            return Ok(category);
        }

        [HttpGet("Name")]
        public async Task<ActionResult<CategoryDto?>> GetByName([FromQuery] string name)
        {
            var category = await mediator.Send(new GetCategoryByNameQuery(name));
            return Ok(category);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRating([FromRoute] int id)
        {
            await mediator.Send(new DeleteCategoryCommand(id));
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRating([FromRoute] int id, [FromBody] UpdateCategoryCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = UserRoles.Owner)]
        public async Task<IActionResult> CreateRating([FromBody] CreateCategoryCommand command)
        {
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }
    }
}
