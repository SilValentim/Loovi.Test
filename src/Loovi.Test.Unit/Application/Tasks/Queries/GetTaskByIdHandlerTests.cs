using AutoMapper;
using FluentAssertions;
using Loovi.Test.Application.Tasks.Common;
using Loovi.Test.Application.Tasks.GetTask;
using Loovi.Test.Domain.Entities;
using Moq;
using Xunit;

namespace Loovi.Test.Unit.Application.Tasks.Queries;

public class GetTaskByIdHandlerTests : IClassFixture<TaskHandlerTestsFixture>
{
    private readonly TaskHandlerTestsFixture _fixture;

    public GetTaskByIdHandlerTests(TaskHandlerTestsFixture fixture)
    {
        _fixture = fixture;
        _fixture.ResetMocks();
    }

    [Fact]
    public async Task Handle_WhenTaskExists_ShouldReturnMappedResult()
    {
        // Arrange
        var query = new GetTaskByIdQuery(Guid.NewGuid());
        var taskEntity = new TaskItem { Id = query.Id, Title = "Test Task" };
        var expectedResult = new TaskResult { Id = query.Id, Title = "Test Task" };

        _fixture.TaskRepositoryMock
            .Setup(r => r.GetByIdAsync(query.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(taskEntity);

        _fixture.MapperMock
            .Setup(m => m.Map<TaskResult>(taskEntity))
            .Returns(expectedResult);

        var handler = new GetTaskByIdHandler(
            _fixture.TaskRepositoryMock.Object,
            _fixture.MapperMock.Object
        );

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);

        _fixture.TaskRepositoryMock.Verify(r => r.GetByIdAsync(query.Id, It.IsAny<CancellationToken>()), Times.Once);
        _fixture.MapperMock.Verify(m => m.Map<TaskResult>(taskEntity), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenTaskDoesNotExist_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        var query = new GetTaskByIdQuery(Guid.NewGuid());

        _fixture.TaskRepositoryMock
            .Setup(r => r.GetByIdAsync(query.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((TaskItem?)null);

        var handler = new GetTaskByIdHandler(
            _fixture.TaskRepositoryMock.Object,
            _fixture.MapperMock.Object
        );

        // Act
        var act = async () => await handler.Handle(query, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Task with ID {query.Id} not found");

        _fixture.TaskRepositoryMock.Verify(r => r.GetByIdAsync(query.Id, It.IsAny<CancellationToken>()), Times.Once);
        _fixture.MapperMock.VerifyNoOtherCalls();
    }
}
