
using AutoMapper;
using Loovi.Test.Application.Tasks.Common;
using Loovi.Test.WebApi.Features.Tasks.Common;

namespace Loovi.Test.WebApi.Features.Tasks.UpdateTask
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            // Map CreateTaskResult to CreateTaskResponse
            CreateMap<TaskResult, TaskResponse>();
        }
    }
}
