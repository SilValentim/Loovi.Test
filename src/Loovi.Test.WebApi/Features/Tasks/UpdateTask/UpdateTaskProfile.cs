
using AutoMapper;
using Loovi.Test.Application.Tasks.CreateTask;
using Loovi.Test.Application.Tasks.UpdateTask;

namespace Loovi.Test.WebApi.Features.Tasks.UpdateTask
{
    public class UpdateTaskProfile : Profile
    {
        public UpdateTaskProfile()
        {
            CreateMap<UpdateTaskRequest, UpdateTaskCommand>();

        }
    }
}
