
using AutoMapper;
using Loovi.Test.Application.Common;
using Loovi.Test.Application.Tasks.Common;
using Loovi.Test.WebApi.Common;
using Loovi.Test.WebApi.Features.Tasks.Common;

namespace Loovi.Test.WebApi.Features.Tasks.UpdateTask
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            // Map CreateTaskResult to CreateTaskResponse
            CreateMap<TaskResult, TaskResponse>()
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreatedAt));

            CreateMap<PaginatedResult<TaskResult>, PaginatedResponse<TaskResponse>>();
        }
    }
}
