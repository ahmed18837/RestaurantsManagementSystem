using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Customers.Commands.AddRestaurantToFavorites;
using Restaurants.Application.Customers.Commands.CreateCustomer;
using Restaurants.Application.Customers.Commands.CreateMultipleCustomers;
using Restaurants.Application.Customers.Commands.DeleteCustomer;
using Restaurants.Application.Customers.Commands.UpdateCustomer;
using Restaurants.Application.Customers.Dtos;
using Restaurants.Application.Customers.Queries.GetAllCustomers;
using Restaurants.Application.Customers.Queries.GetCustomerByEmail;
using Restaurants.Application.Customers.Queries.GetCustomerById;
using Restaurants.Application.Customers.Queries.GetCustomerByName;
using Restaurants.Application.Customers.Queries.GetCustomerByPhoneNumber;
using Restaurants.Application.Customers.Queries.GetCustomerFavoriteRestaurants;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Exceptions;
using System.Data;

namespace Restaurants.API.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController(IMediator mediator) : ControllerBase
    {

        [HttpPost("bulk")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> CreateMultipleCustomers([FromBody] CreateMultipleCustomersCommand command)
        {
            var ids = await mediator.Send(command);
            return Ok(new { CreatedIds = ids });
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll([FromQuery] GetAllCustomersQuery query)
        {
            var customers = await mediator.Send(query);
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto?>> GetById([FromRoute] int id)
        {
            var customer = await mediator.Send(new GetCustomerByIdQuery(id));
            return Ok(customer);
        }

        [HttpGet("Name")]
        public async Task<ActionResult<CustomerDto?>> GetByName([FromQuery] string name = "Ahmed Ali")
        {
            var customer = await mediator.Send(new GetCustomerByNameQuery(name));
            return Ok(customer);
        }

        [HttpGet("PhoneNumber")]
        public async Task<ActionResult<CustomerDto?>> GetByPhoneNumber([FromQuery] string phoneNumber = "0100000001")
        {
            var customer = await mediator.Send(new GetCustomerByPhoneNumberQuery(phoneNumber));
            return Ok(customer);
        }

        [HttpGet("Email")]
        public async Task<ActionResult<CustomerDto?>> GetByEmail([FromQuery] string email = "user@gmail.com")
        {
            var customer = await mediator.Send(new GetCustomerByEmailQuery(email));
            return Ok(customer);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            await mediator.Send(new DeleteCustomerCommand(id));
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRating([FromRoute] int id, [FromBody] UpdateCustomerCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPost("AddRestaurantToFavorite")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddFavorite([FromBody] AddRestaurantToFavoritesCommand command)
        {
            try
            {
                await mediator.Send(command);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (DuplicateNameException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("Favorites")]
        public async Task<ActionResult<List<RestaurantDto>>> GetFavorites()
        {
            var result = await mediator.Send(new GetCustomerFavoriteRestaurantsQuery());
            return Ok(result);
        }
    }
}
