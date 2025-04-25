using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Application.Tasks.CreateTask
{
    public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskValidator()
        {
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
                .GreaterThan(x => DateTime.UtcNow).WithMessage("The DueDate must be a future date.");
        }
    }
}
