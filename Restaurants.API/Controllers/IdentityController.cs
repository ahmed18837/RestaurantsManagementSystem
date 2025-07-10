using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.User.Commands.AssignRoleToUser;
using Restaurants.Application.User.Commands.DisableRefreshToken;
using Restaurants.Application.User.Commands.ForgetPassword;
using Restaurants.Application.User.Commands.LoginUser;
using Restaurants.Application.User.Commands.RefreshToken;
using Restaurants.Application.User.Commands.RegisterUser;
using Restaurants.Application.User.Commands.Resend2FACode;
using Restaurants.Application.User.Commands.ResetPassword;
using Restaurants.Application.User.Commands.Send2FACode;
using Restaurants.Application.User.Commands.Verify2FACode;

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
    }
}
