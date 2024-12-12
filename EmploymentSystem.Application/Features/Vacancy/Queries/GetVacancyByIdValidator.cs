using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Features.Vacancy.Queries;
public class GetVacancyByIdValidator : AbstractValidator<GetVacancyByIdQuery> 
{
    public GetVacancyByIdValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must have value.");
    }
}
