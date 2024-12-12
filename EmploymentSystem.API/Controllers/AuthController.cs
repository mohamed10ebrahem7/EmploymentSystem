using EmploymentSystem.Application.Dtos.Request;
using EmploymentSystem.Application.Features.Login;
using EmploymentSystem.Application.Features.User.Commands.AddUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginReqDto info)
        {
            var result = await _mediator.Send(new LoginUserCommand { info = info });
            return Ok(new { Message = result });
        }
    }
}
