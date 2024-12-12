using EmploymentSystem.Application.Dtos.Request;
using EmploymentSystem.Application.Features.Vacancy.Commands;
using EmploymentSystem.Application.Features.Vacancy.Queries;
using EmploymentSystem.Application.Features.Vacancy.Queriesl;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacanciesController : ControllerBase
    {

        private readonly IMediator _mediator;
        public VacanciesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Employer")]
        [HttpPost("AddVacancy")]
        public async Task<IActionResult> CreateVacancy([FromBody] VacancyDto vacancyDto)
        {
            var result = await _mediator.Send(new CreateVacancyCommand { info = vacancyDto });
            return Ok(result);
        }

        [Authorize(Roles = "Employer")]
        [HttpPut("UpdateVacancy")]
        public async Task<IActionResult> UpdateVacancy([FromBody] VacancyDto vacancy)
        {

            var result = await _mediator.Send(new UpdateVacancyCommand { info = vacancy });

            if (result.result is bool booleanValue)
            {
                if (!booleanValue)
                {
                    return NotFound(result);
                }
            }

            return Ok(result);
        }

        [Authorize(Roles = "Employer")]
        [HttpDelete("DeleteVacancy/{id}")]
        public async Task<IActionResult> DeleteVacancy(int id)
        {
            var result = await _mediator.Send(new DeleteVacancyCommand { Id = id });

            if (result.result is bool booleanValue)
            {
                if (!booleanValue)
                {
                    return NotFound(result);
                }
            }

            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetVacancy/{id}")]
        public async Task<IActionResult> GetVacancy(int id)
        {
            var result = await _mediator.Send(new GetVacancyByIdQuery { Id = id });
            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetAllVacancies")]
        public async Task<IActionResult> GetAllVacancies()
        {
            var vacancies = await _mediator.Send(new GetAllVacanciesQuery());
            return Ok(vacancies);
        }

        [Authorize(Roles = "Employer")]
        [HttpGet("GetApplicantsForVacancy/{id}")]
        public async Task<IActionResult> GetApplicantsForVacancy(int id)
        {
            var response = await _mediator.Send(new GetApplicantsForVacancyQuery { Id = id });
            return Ok(response);
        }

        [Authorize]
        [HttpGet("SearchVacancies/{name}")]
        public async Task<IActionResult> GetVacancyByName(string name)
        {
            var response = await _mediator.Send(new GetVacancyByNameQuery { Name = name });
            return Ok(response);
        }
    }
}
