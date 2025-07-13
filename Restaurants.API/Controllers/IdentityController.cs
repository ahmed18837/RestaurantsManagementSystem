using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.User.Commands.AssignRoleToUser;
using Restaurants.Application.User.Commands.ChangePassword;
using Restaurants.Application.User.Commands.DisableRefreshToken;
using Restaurants.Application.User.Commands.ForgetPassword;
using Restaurants.Application.User.Commands.LoginUser;
using Restaurants.Application.User.Commands.RefreshToken;
using Restaurants.Application.User.Commands.RegisterUser;
using Restaurants.Application.User.Commands.Resend2FACode;
using Restaurants.Application.User.Commands.ResetPassword;
using Restaurants.Application.User.Commands.Send2FACode;
using Restaurants.Application.User.Commands.UnlockUser;
using Restaurants.Application.User.Commands.UpdateUser;
using Restaurants.Application.User.Commands.Verify2FACode;
using Restaurants.Application.User.Dtos;
using Restaurants.Application.User.Queries.GetAllRoles;
using Restaurants.Application.User.Queries.GetAllUsers;
using Restaurants.Application.User.Queries.GetUserByEmail;
using Restaurants.Application.User.Queries.GetUserById;
using Restaurants.Application.User.Queries.GetUserByName;
using Restaurants.Application.User.Queries.GetUserByPhoneNumber;
using Restaurants.Application.User.Queries.GetUsersByRole;
using Restaurants.Domain.Constants;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController(IMediator mediator) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            await mediator.Send(command);

            return Ok("User was registered ... Please login.");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("Send2FACode")]
        public async Task<IActionResult> Send2FACode([FromBody] Send2FACodeCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("Resend2FACode")]
        public async Task<IActionResult> Resend2FACode([FromBody] Resend2FACodeCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("Verify2FACode")]
        public async Task<IActionResult> Verify2FACode([FromBody] Verify2FACodeCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleToUserCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("DisableRefreshToken")]
        public async Task<IActionResult> DisableRefreshToken([FromBody] DisableRefreshTokenCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll([FromQuery] GetAllUsersQuery query)
        {
            var users = await mediator.Send(query);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUserById([FromRoute] string id)
        {
            var user = await mediator.Send(new GetUserByIdQuery(id));
            return Ok(user);
        }

        [HttpGet("Email")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUserByEmail([FromQuery] string email = "user@gmail.com")
        {
            var user = await mediator.Send(new GetUserByEmailQuery(email));
            return Ok(user);
        }

        [HttpGet("Name")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUserByName([FromQuery] string name)
        {
            var user = await mediator.Send(new GetUserByNameQuery(name));
            return Ok(user);
        }

        [HttpGet("PhoneNumber")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUserByPhoneNumber([FromQuery] string phoneNumber)
        {
            var user = await mediator.Send(new GetUserByPhoneNumberQuery(phoneNumber));
            return Ok(user);
        }

        [HttpPost("UnlockUser")]
        public async Task<IActionResult> UnlockUser([FromBody] UnlockUserCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateRating([FromRoute] string id, [FromBody] UpdateUserCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }

        [HttpGet("AllRoles")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllRoles()
        {
            var user = await mediator.Send(new GetAllRolesQuery());
            return Ok(user);
        }

        [HttpGet("Role")]
        public ActionResult<IEnumerable<UserDto>> GetUsersByRole([FromRoute] string role, [FromQuery] string? searchPhrase = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 5,
        [FromQuery] string? sortBy = null,
        [FromQuery] SortDirection sortDirection = SortDirection.Ascending)
        {
            var query = new GetUsersByRoleQuery(role)
            {
                SearchPhrase = searchPhrase,
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortBy = sortBy,
                SortDirection = sortDirection
            };
            return Ok(query);
        }
    }
}
