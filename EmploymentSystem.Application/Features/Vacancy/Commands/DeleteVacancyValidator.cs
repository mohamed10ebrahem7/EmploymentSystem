using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Features.Vacancy.Commands;
public class DeleteVacancyValidator : AbstractValidator<DeleteVacancyCommand>
{
    public DeleteVacancyValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must have value.");
    }
}
