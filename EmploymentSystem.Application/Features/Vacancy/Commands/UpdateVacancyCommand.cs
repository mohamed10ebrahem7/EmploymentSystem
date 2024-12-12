using EmploymentSystem.Application.Dtos.Request;
using EmploymentSystem.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Features.Vacancy.Commands;
public class UpdateVacancyCommand : IRequest<DefaultResult>
{
    public VacancyDto info { get; set; }
}

