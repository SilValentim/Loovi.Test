using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Loovi.Test.Application.Tasks.Common;
using Loovi.Test.Application.Tasks.UpdateTask;
using Loovi.Test.Domain.Entities;
using Loovi.Test.Domain.Enums;
using Loovi.Test.Unit.Application.Tasks.Builders;

namespace Loovi.Test.Unit.Application.Tasks.Commands;

public class UpdateTaskHandlerTests : IClassFixture<TaskHandlerTestsFixture>
{
    private readonly TaskHandlerTestsFixture _fixture;
    private readonly UpdateTaskHandler _handler;

    public UpdateTaskHandlerTests(TaskHandlerTestsFixture fixture)
    {
        _fixture = fixture;
        _handler = new UpdateTaskHandler(_fixture.TaskRepositoryMock.Object, _fixture.MapperMock.Object);
    }

    [Fact]
    public async Task Handle_WhenValidUpdateCommand_ShouldUpdateTaskAndReturnResult()
    {
        _fixture.ResetMocks();

        // Arrange
        var command = UpdateTaskCommandBuilder.New().Build();
        var taskEntity = new TaskItem
        {
            Id = command.Id,
            Title = command.Title,
            Description = command.Description,
            DueDate = command.DueDate,
            Status = TaskItemStatus.Pending
        };

        _fixture.MapperMock.Setup(m => m.Map<TaskItem>(command)).Returns(taskEntity);
        _fixture.TaskRepositoryMock
            .Setup(r => r.ExistsSameTitleAndDifferentIdAsync(It.IsAny<TaskItem>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        _fixture.TaskRepositoryMock
            .Setup(r => r.UpdateAsync(taskEntity, It.IsAny<CancellationToken>()))
            .ReturnsAsync(taskEntity);
        _fixture.MapperMock.Setup(m => m.Map<TaskResult>(taskEntity)).Returns(new TaskResult());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();

        _fixture.TaskRepositoryMock.Verify(r => r.ExistsSameTitleAndDifferentIdAsync(It.IsAny<TaskItem>(), It.IsAny<CancellationToken>()), Times.Once);
        _fixture.TaskRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<TaskItem>(), It.IsAny<CancellationToken>()), Times.Once);
        _fixture.MapperMock.Verify(m => m.Map<TaskItem>(command), Times.Once);
        _fixture.MapperMock.Verify(m => m.Map<TaskResult>(taskEntity), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenSameTitleExists_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var command = UpdateTaskCommandBuilder.New().Build();
        var taskEntity = new TaskItem();

        _fixture.MapperMock.Setup(m => m.Map<TaskItem>(command)).Returns(taskEntity);
        _fixture.TaskRepositoryMock
            .Setup(r => r.ExistsSameTitleAndDifferentIdAsync(It.IsAny<TaskItem>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Task with title '{command.Title}' already exists");

        _fixture.TaskRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<TaskItem>(), It.IsAny<CancellationToken>()), Times.Never);
        _fixture.MapperMock.Verify(m => m.Map<TaskResult>(It.IsAny<TaskItem>()), Times.Never);
    }
}
