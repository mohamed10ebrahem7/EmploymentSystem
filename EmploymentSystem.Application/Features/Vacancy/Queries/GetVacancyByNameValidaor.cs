using EmploymentSystem.Application.Features.Vacancy.Queriesl;
using EmploymentSystem.Application.Infrastructure;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Features.Vacancy.Queries;
public class GetVacancyByNameValidaor : AbstractValidator<GetVacancyByNameQuery>
{
    public GetVacancyByNameValidaor()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Enter search value.");
    }
}
