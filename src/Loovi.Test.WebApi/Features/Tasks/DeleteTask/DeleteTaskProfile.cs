
using AutoMapper;
using Loovi.Test.Application.Tasks.CreateTask;
using Loovi.Test.Application.Tasks.DeleteTask;

namespace Loovi.Test.WebApi.Features.Tasks.UpdateTask
{
    public class DeleteTaskProfile : Profile
    {
        public DeleteTaskProfile()
        {
            CreateMap<Guid, DeleteTaskCommand>()
            .ConstructUsing(id => new DeleteTaskCommand(id));

        }
    }
}
