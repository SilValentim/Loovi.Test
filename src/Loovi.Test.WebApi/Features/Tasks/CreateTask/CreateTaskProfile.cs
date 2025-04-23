
using AutoMapper;
using Loovi.Test.Application.Tasks.CreateTask;
using Loovi.Test.WebApi.Features.Tasks.Common;

namespace Loovi.Test.WebApi.Features.Tasks.CreateTask
{
    public class CreateTaskProfile : Profile
    {
        public CreateTaskProfile()
        {
            // Map CreateTaskRequest to CreateTaskCommand
            CreateMap<CreateTaskRequest, CreateTaskCommand>();

            // Map CreateTaskResult to CreateTaskResponse
            CreateMap<CreateTaskResult, TaskResponse>();
        }
    }
}
