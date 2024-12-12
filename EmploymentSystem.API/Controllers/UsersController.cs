using EmploymentSystem.Application.Dtos.Request;
using EmploymentSystem.Application.Features.User.Commands.AddUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace EmploymentSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddUser")]
    public async Task<IActionResult> CreateUser([FromBody] UserInfoReqDto info)
    {
        var result = await _mediator.Send(new AddUserCommand{info = info});
        return Ok(result);
    }
}

