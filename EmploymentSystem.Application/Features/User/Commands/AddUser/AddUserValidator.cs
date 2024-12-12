using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Features.User.Commands.AddUser
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserValidator()
        {
            RuleFor(r => r.info.FullName).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(r => r.info.JobTitle).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.info.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^(\+?[1-9]\d{1,14}|0\d{1,14})$").WithMessage("Invalid phone number format.");
            RuleFor(x => x.info.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
                .Matches(@"[\W_]").WithMessage("Password must contain at least one special character (e.g., @, #, $, etc.).");

            RuleFor(r => r.info.Email).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(r => r.info.Role).NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
