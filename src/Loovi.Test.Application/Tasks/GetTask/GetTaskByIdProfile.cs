using AutoMapper;
using Loovi.Test.Application.Tasks.Common;
using Loovi.Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Application.Tasks.GetTask
{
    class GetTaskByIdProfile : Profile
    {
        public GetTaskByIdProfile()
        {
            CreateMap<GetTaskByIdQuery, TaskItem>();
        }
    }
}
