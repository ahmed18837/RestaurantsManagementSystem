using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Categories.Dtos;
using Restaurants.Application.Categories.Queries.GetAllCategories;
using Restaurants.Application.Categories.Queries.GetCategoryById;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
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
    }
}
