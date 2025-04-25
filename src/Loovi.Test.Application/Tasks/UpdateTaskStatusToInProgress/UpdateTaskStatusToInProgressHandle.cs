using AutoMapper;
using Loovi.Test.Application.Tasks.Common;
using Loovi.Test.Application.Tasks.UpdateTaskStatusToComplete;
using Loovi.Test.Domain.Repositories;
using MediatR;
using System.Net;

namespace Loovi.Test.Application.Tasks.UpdateTaskStatusToInProgress;

public class UpdateTaskStatusToInProgressHandle : IRequestHandler<UpdateTaskStatusToInProgressCommand, TaskResult>
{
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IMapper _mapper;

    public UpdateTaskStatusToInProgressHandle(ITaskItemRepository taskItemRepository, IMapper mapper)
    {
        _taskItemRepository = taskItemRepository;
        _mapper = mapper;
    }

    public async Task<TaskResult> Handle(UpdateTaskStatusToInProgressCommand request, CancellationToken cancellationToken)
    {

        var task = 
            await _taskItemRepository
            .ChangeTaskStatusToInProgressAsync(request.Id, cancellationToken);


        return _mapper.Map<TaskResult>(task);
    }
}
