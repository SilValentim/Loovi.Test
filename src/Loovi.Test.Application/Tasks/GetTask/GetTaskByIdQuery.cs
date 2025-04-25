using Loovi.Test.Application.Tasks.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Application.Tasks.GetTask
{
    public class GetTaskByIdQuery : IRequest<TaskResult>
    {
        public GetTaskByIdQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    } 
}
