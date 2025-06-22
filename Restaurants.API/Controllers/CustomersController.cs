using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Customers.Dtos;
using Restaurants.Application.Customers.Queries.GetAllCustomers;
using Restaurants.Application.Customers.Queries.GetCustomerById;
using Restaurants.Application.Customers.Queries.GetCustomerByName;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController(IMediator mediator) : ControllerBase
    {
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

        [HttpGet("Name{name}")]
        public async Task<ActionResult<CustomerDto?>> GetByName([FromRoute] string name)
        {
            var customer = await mediator.Send(new GetCustomerByNameQuery(name));
            return Ok(customer);
        }
    }
}
