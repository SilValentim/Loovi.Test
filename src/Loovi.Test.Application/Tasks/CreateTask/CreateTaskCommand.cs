using Loovi.Test.Application.Tasks.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Application.Tasks.CreateTask
{
    public class CreateTaskCommand : IRequest<TaskResult>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    } 
}
