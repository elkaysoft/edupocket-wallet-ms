using Edupocket.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edupocket.Application.Validations
{
    public class CreateWalletValidator: AbstractValidator<CreateWalletCommand>
    {
        public CreateWalletValidator()
        {
            RuleFor(x => x.EmailAddress)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Email Address is required")
                .EmailAddress()
                .WithMessage("Invalid Email address");

            RuleFor(x => x.FirstName)
                 .Cascade(CascadeMode.Stop)
                 .NotEmpty()
                 .WithMessage("First name is required");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Last name is required");

            RuleFor(x => x.MobileNumber)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Mobile Number is required")
                .Matches(@"\d")
                .WithMessage("Invalid Mobile Number format");

            RuleFor(x => x.Gender)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Gender is required");
        }
    }
}
