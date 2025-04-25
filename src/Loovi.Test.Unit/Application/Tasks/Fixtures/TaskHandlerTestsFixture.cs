using AutoMapper;
using Loovi.Test.Application.Tasks.CreateTask;
using Loovi.Test.Domain.Repositories;
using Loovi.Test.ORM;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;

public class TaskHandlerTestsFixture
{
    public Mock<ITaskItemRepository> TaskRepositoryMock { get; }
    public Mock<IMapper> MapperMock { get; }

    public CreateTaskHandler CreateTaskHandler => new(TaskRepositoryMock.Object, MapperMock.Object);

    public TaskHandlerTestsFixture()
    {
        TaskRepositoryMock = new Mock<ITaskItemRepository>(MockBehavior.Strict);
        MapperMock = new Mock<IMapper>(MockBehavior.Strict);
    }

    public void ResetMocks()
    {
        TaskRepositoryMock.Reset();
        MapperMock.Reset();
    }
}