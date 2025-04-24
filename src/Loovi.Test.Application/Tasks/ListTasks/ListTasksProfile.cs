using AutoMapper;
using Loovi.Test.Application.Common;
using Loovi.Test.Application.Tasks.Common;
using Loovi.Test.Domain.Common;
using Loovi.Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Application.Tasks.ListTasks
{
    class ListTasksProfile : Profile
    {
        public ListTasksProfile()
        {
            CreateMap<Paginated<TaskItem>, PaginatedResult<TaskResult>>();
        }
    }
}
