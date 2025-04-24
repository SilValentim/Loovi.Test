using FluentValidation;
using Loovi.Test.Application.Tasks.DeleteTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Application.Tasks.GetTask
{
    public class GetTaskValidator : AbstractValidator<GetTaskCommand>
    {
        public GetTaskValidator()
        {
            RuleFor(request => request.Id)
                .NotNull().WithMessage("The Id is required.")
                .NotEmpty().WithMessage("The Id is required.");
        }
    }
}
