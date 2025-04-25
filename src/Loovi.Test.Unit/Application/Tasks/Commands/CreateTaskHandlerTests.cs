using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Loovi.Test.Unit.Application.Tasks;
using Loovi.Test.Unit.Application.Tasks.Commands;
using Loovi.Test.Application.Tasks.Common;
using Loovi.Test.Domain.Entities;
using Moq;
using Loovi.Test.Unit.Application.Tasks.Builders;

namespace Loovi.Test.Unit.Application.Tasks.Commands;

[CollectionDefinition("TaskHandlers")]
public class TaskHandlersCollection : ICollectionFixture<TaskHandlerTestsFixture> { }

[Collection("TaskHandlers")]
public class CreateTaskHandlerTests
{
    private readonly TaskHandlerTestsFixture _fixture;

    public CreateTaskHandlerTests(TaskHandlerTestsFixture fixture)
    {
        _fixture = fixture;
        _fixture.ResetMocks(); // limpa mocks entre testes
    }

    [Fact]
    public async Task Handle_ShouldCreateTask_WhenTitleIsUnique()
    {
        // Arrange
        var command = new CreateTaskCommandBuilder()
            .WithTitle("Unique Task")
            .WithDescription("This task is unique.")
            .WithDueDate(DateTime.UtcNow.AddDays(2))
            .Build();

        var taskEntity = new TaskItem { Title = command.Title };
        var taskResult = new TaskResult { Title = command.Title };

        _fixture.TaskRepositoryMock
            .Setup(r => r.ExistsByTitleAsync(command.Title, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _fixture.MapperMock
            .Setup(m => m.Map<TaskItem>(command))
            .Returns(taskEntity);

        _fixture.TaskRepositoryMock
            .Setup(r => r.CreateAsync(taskEntity, It.IsAny<CancellationToken>()))
            .ReturnsAsync(taskEntity);

        _fixture.MapperMock
            .Setup(m => m.Map<TaskResult>(taskEntity))
            .Returns(taskResult);

        // Act
        var result = await _fixture.CreateTaskHandler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be("Unique Task");

        _fixture.TaskRepositoryMock.VerifyAll();
        _fixture.MapperMock.VerifyAll();
    }

    [Fact]
    public async Task Handle_ShouldThrowInvalidOperationException_WhenTitleAlreadyExists()
    {
        // Arrange
        var command = new CreateTaskCommandBuilder()
            .WithTitle("Duplicated Task")
            .Build();

        _fixture.TaskRepositoryMock
            .Setup(r => r.ExistsByTitleAsync(command.Title, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        Func<Task> act = async () => await _fixture.CreateTaskHandler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Task with title '{command.Title}' already exists");

        _fixture.TaskRepositoryMock.Verify(r => r.ExistsByTitleAsync(command.Title, It.IsAny<CancellationToken>()), Times.Once);
        _fixture.MapperMock.VerifyNoOtherCalls();
    }
}