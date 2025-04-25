using Xunit;
using FluentValidation.TestHelper;
using System;
using Loovi.Test.Application.Tasks.CreateTask;
using Loovi.Test.Unit.Application.Tasks.Builders;

namespace Loovi.Test.Unit.Application.Tasks.Validators;

public class CreateTaskValidatorTests
{
    private readonly CreateTaskValidator _validator;

    public CreateTaskValidatorTests()
    {
        _validator = new CreateTaskValidator();
    }

    [Fact]
    public void Validate_WhenTitleIsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var command = CreateTaskCommandBuilder
            .New()
            .WithTitle("")
            .Build();

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public void Validate_WhenTitleExceedsMaxLength_ShouldHaveValidationError()
    {
        var command = CreateTaskCommandBuilder
            .New()
            .WithTitle(new string('A', 101))
            .Build();

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public void Validate_WhenDescriptionExceedsMaxLength_ShouldHaveValidationError()
    {
        var command = CreateTaskCommandBuilder
            .New()
            .WithDescription(new string('D', 1001))
            .Build();

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Fact]
    public void Validate_WhenDueDateIsInPast_ShouldHaveValidationError()
    {
        var command = CreateTaskCommandBuilder
            .New()
            .WithDueDate(DateTime.UtcNow.AddMinutes(-1))
            .Build();

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.DueDate);
    }

    [Fact]
    public void Validate_WhenCommandIsValid_ShouldNotHaveValidationErrors()
    {
        var command = CreateTaskCommandBuilder.New().Build();

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
