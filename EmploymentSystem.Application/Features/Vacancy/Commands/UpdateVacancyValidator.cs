using FluentValidation;

namespace EmploymentSystem.Application.Features.Vacancy.Commands;
public class UpdateVacancyValidator : AbstractValidator<UpdateVacancyCommand>
{
    public UpdateVacancyValidator()
    {

        RuleFor(x => x.info.Id)
            .GreaterThan(0).WithMessage("Id must have value.");

        RuleFor(x => x.info.Title)
            .Length(1, 100).WithMessage("{PropertyName} must be between 1 and 100 characters.");

        RuleFor(x => x.info.Description)
            .Length(1, 1000).WithMessage("{PropertyName} must be between 1 and 1000 characters.");

        RuleFor(x => x.info.MaxApplications)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(x => x.info.ExpiryDate)
            .GreaterThan(DateTime.Today).WithMessage("{PropertyName} must be in the future.");
    }
}
