using AutoMapper;
using Loovi.Test.Application.Tasks.Common;
using Loovi.Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Application.Tasks.UpdateTask
{
    class UpdateTaskProfile : Profile
    {
        public UpdateTaskProfile()
        {
            CreateMap<UpdateTaskCommand, TaskItem>();
        }
    }
}
