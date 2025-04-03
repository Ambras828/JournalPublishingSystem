using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Application.DTOs;
using FluentValidation.AspNetCore;

namespace Application.Validators
{
    public class UpdateUserDTOValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDTOValidator()
        {
            RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

            RuleFor(x => x.Username)
                .NotEmpty().MinimumLength(3)
                .WithMessage("Username must be at least 3 characters long.");

            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("Invalid email address.");

            RuleFor(x => x.FullName)
                .NotEmpty().MinimumLength(3)
                .WithMessage("Full name must be at least 3 characters long.");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\d{10,15}$")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber))
                .WithMessage("Phone number must be 10-15 digits.");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive cannot be null.");
        }
    }
}
