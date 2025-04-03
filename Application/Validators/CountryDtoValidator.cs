using Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;

namespace Application.Validators
{
    public class CountryDtoValidator: AbstractValidator<CountryDto>
    {
        public CountryDtoValidator() {

            RuleFor(x => x.CountryName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.CountryCode).NotEmpty().MaximumLength(100);
        }
    }
}
