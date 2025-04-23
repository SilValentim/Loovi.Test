
using AutoMapper;
using Loovi.Test.Application.Tasks.CreateTask;

namespace Loovi.Test.WebApi.Features.Tasks.UpdateTask
{
    public class UpdateTaskProfile : Profile
    {
        public UpdateTaskProfile()
        {
            // Map CreateTaskRequest to CreateTaskCommand
            CreateMap<UpdateTaskRequest, CreateTaskCommand>();

            // Map CreateTaskResult to CreateTaskResponse
            CreateMap<TaskResult, TaskResponse>();
        }
    }
}
