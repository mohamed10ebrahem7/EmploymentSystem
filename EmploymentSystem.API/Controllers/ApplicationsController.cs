using EmploymentSystem.Application.Dtos.Request;
using EmploymentSystem.Application.Features.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystem.API.Controllers
{
    [Authorize(Roles = "Applicant")]
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApplicationsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("AddApplication")]
        public async Task<IActionResult> CreateApplication([FromBody] ApplicationDto applicationDto)
        {
            var applicationId = await _mediator.Send(new CreateApplicationCommand { Application = applicationDto });
            return Ok(applicationId);
        }
    }
}
