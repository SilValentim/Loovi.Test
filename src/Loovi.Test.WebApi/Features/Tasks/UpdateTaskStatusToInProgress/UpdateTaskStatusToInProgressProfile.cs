
using AutoMapper;
using Loovi.Test.Application.Tasks.CreateTask;
using Loovi.Test.Application.Tasks.DeleteTask;
using Loovi.Test.Application.Tasks.UpdateTaskStatusToInProgress;

namespace Loovi.Test.WebApi.Features.Tasks.UpdateTaskStatusToInProgress
{
    public class UpdateTaskStatusToInProgressProfile : Profile
    {
        public UpdateTaskStatusToInProgressProfile()
        {
            CreateMap<Guid, UpdateTaskStatusToInProgressCommand>()
            .ConstructUsing(id => new UpdateTaskStatusToInProgressCommand(id));

        }
    }
}
