using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Application.Auth.AuthenticateUser
{
    public class AuthenticateUserValidator : AbstractValidator<AuthenticateUserCommand>
    {
        public AuthenticateUserValidator()
        {
            RuleFor(request => request.Username)
                .NotEmpty().WithMessage("The Username is required.");

            RuleFor(request => request.Password)
                .NotEmpty().WithMessage("The Password is required.");
        }
    }
}
