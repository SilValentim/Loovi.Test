using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Application.Tasks.UpdateTaskStatusToComplete
{
    public class UpdateTaskStatusToCompleteValidator : AbstractValidator<UpdateTaskStatusToCompleteCommand>
    {
        public UpdateTaskStatusToCompleteValidator()
        {
            RuleFor(request => request.Id)
                .NotNull().WithMessage("The Id is required.")
                .NotEmpty().WithMessage("The Id is required.");
        }
    }
}
