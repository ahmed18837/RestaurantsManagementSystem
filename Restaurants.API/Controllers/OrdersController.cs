using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Orders.Commands.CreateOrder;
using Restaurants.Application.Orders.Commands.DeleteOrder;
using Restaurants.Application.Orders.Commands.UpdateOrder;
using Restaurants.Application.Orders.Dtos;
using Restaurants.Application.Orders.Queries.GetAllOrders;
using Restaurants.Application.Orders.Queries.GetOrderById;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll([FromQuery] GetAllOrdersQuery query)
        {
            var orders = await mediator.Send(query);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto?>> GetById([FromRoute] int id)
        {
            var order = await mediator.Send(new GetOrderByIdQuery(id));
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPatch("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] UpdateOrderCommand command)
        {
            command.Id = Id;

            await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRating([FromRoute] int id)
        {
            await mediator.Send(new DeleteOrderCommand(id));
            return NoContent();
        }
    }
}
