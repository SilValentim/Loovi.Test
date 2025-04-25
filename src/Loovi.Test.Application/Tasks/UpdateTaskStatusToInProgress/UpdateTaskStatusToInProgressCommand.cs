using Loovi.Test.Application.Tasks.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Application.Tasks.UpdateTaskStatusToInProgress
{
    public class UpdateTaskStatusToInProgressCommand : IRequest<TaskResult>
    {
        public Guid Id { get; set; }

        public UpdateTaskStatusToInProgressCommand(Guid id)
        {
            Id = id;
        }
    } 
}
