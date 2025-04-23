using FluentValidation;
using Loovi.Test.Application.Tasks.CreateTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Application.Tasks.UpdateTask
{
    public class UpdateTaskValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskValidator()
        {

            RuleFor(request => request.Id)
                .NotNull().WithMessage("The Id is required.")
                .NotEmpty().WithMessage("The Id is required.");

            // Title: Required and maximum length of 100 characters
            RuleFor(request => request.Title)
                .NotEmpty().WithMessage("The Title is required.")
                .MaximumLength(100).WithMessage("The Title must not exceed 100 characters.");

            // Description: Optional but must not exceed 1000 characters
            RuleFor(request => request.Description)
                .MaximumLength(1000).WithMessage("The Description must not exceed 1000 characters.");

            // DueDate: Required and must be a future date
            RuleFor(request => request.DueDate)
                .NotEmpty().WithMessage("The DueDate is required.")
                .GreaterThan(DateTime.Now).WithMessage("The DueDate must be a future date.");
        }
    }
}
