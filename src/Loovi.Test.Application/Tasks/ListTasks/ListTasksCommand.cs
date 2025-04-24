using Loovi.Test.Application.Common;
using Loovi.Test.Application.Tasks.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Application.Tasks.ListTasks
{
    public class ListTasksCommand : IRequest<PaginatedResult<TaskResult>>
    {
        public ListTasksCommand(IDictionary<string, string[]> parameters)
        {
            Parameters = parameters;
        }
        public IDictionary<string, string[]> Parameters { get; set; }
    }
}
