using FluentValidation;
using Loovi.Test.Application.Tasks.UpdateTaskStatusToInProgress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Application.Tasks.UpdateTaskStatusToComplete
{
    public class UpdateTaskStatusToInProgressValidator : AbstractValidator<UpdateTaskStatusToInProgressCommand>
    {
        public UpdateTaskStatusToInProgressValidator()
        {
            RuleFor(request => request.Id)
                .NotNull().WithMessage("The Id is required.")
                .NotEmpty().WithMessage("The Id is required.");
        }
    }
}
