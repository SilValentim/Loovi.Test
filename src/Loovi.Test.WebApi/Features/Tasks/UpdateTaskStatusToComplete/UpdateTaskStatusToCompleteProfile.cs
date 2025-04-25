
using AutoMapper;
using Loovi.Test.Application.Tasks.CreateTask;
using Loovi.Test.Application.Tasks.DeleteTask;
using Loovi.Test.Application.Tasks.UpdateTaskStatusToComplete;

namespace Loovi.Test.WebApi.Features.Tasks.UpdateTaskStatusToComplete
{
    public class UpdateTaskStatusToCompleteProfile : Profile
    {
        public UpdateTaskStatusToCompleteProfile()
        {
            CreateMap<Guid, UpdateTaskStatusToCompleteCommand>()
            .ConstructUsing(id => new UpdateTaskStatusToCompleteCommand(id));

        }
    }
}
