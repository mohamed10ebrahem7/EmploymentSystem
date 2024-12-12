using EmploymentSystem.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Features.Vacancy.Queries;
public class GetApplicantsForVacancyQuery : IRequest<DefaultResult>
{
    public int Id { get; set; }
}
