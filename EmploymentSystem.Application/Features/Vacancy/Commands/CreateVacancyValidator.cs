using EmploymentSystem.Application.Features.User.Commands.AddUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Features.Vacancy.Commands;
public class CreateVacancyValidator : AbstractValidator<CreateVacancyCommand>
{
    public CreateVacancyValidator()
    {
        RuleFor(x => x.info.Title)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Length(1, 100).WithMessage("{PropertyName} must be between 1 and 100 characters.");

        RuleFor(x => x.info.Description)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Length(1, 1000).WithMessage("{PropertyName} must be between 1 and 1000 characters.");

        RuleFor(x => x.info.MaxApplications)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(x => x.info.ExpiryDate)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .GreaterThan(DateTime.Today).WithMessage("{PropertyName} must be in the future.");

        RuleFor(x => x.info.EmployerId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .GreaterThan(0).WithMessage("{PropertyName} must be valid.");
    }
}
