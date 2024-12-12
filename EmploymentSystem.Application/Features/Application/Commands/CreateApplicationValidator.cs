using FluentValidation;

namespace EmploymentSystem.Application.Features.Application.Commands;
public class CreateApplicationValidator : AbstractValidator<CreateApplicationCommand>
{
    public CreateApplicationValidator()
    {

        RuleFor(x => x.Application.ApplicantId)
            .GreaterThan(0).WithMessage("ApplicantId must have value.");

        RuleFor(x => x.Application.VacancyId)
            .GreaterThan(0).WithMessage("VacancyId must have value.");
    }
}
