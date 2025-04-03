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
    public class CreateUserDTOValidator:AbstractValidator<CreateUserDTO>
    {
        public CreateUserDTOValidator()
            {
                RuleFor(x => x.Username).NotEmpty().MaximumLength(100);
                RuleFor(x => x.email).NotEmpty().EmailAddress().MaximumLength(100);
                RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
                RuleFor(x => x.phoneNumber).MaximumLength(50).When(x => !string.IsNullOrEmpty(x.phoneNumber));
            }
      

    }
}
