using AutoMapper;
using Loovi.Test.Application.Tasks.Common;
using Loovi.Test.Application.Tasks.UpdateTaskStatusToComplete;
using Loovi.Test.Domain.Repositories;
using MediatR;
using System.Net;

namespace Loovi.Test.Application.Tasks.ChangeStatus;

public class UpdateTaskStatusToCompleteHandle : IRequestHandler<UpdateTaskStatusToCompleteCommand, TaskResult>
{
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IMapper _mapper;

    public UpdateTaskStatusToCompleteHandle(ITaskItemRepository taskItemRepository, IMapper mapper)
    {
        _taskItemRepository = taskItemRepository;
        _mapper = mapper;
    }

    public async Task<TaskResult> Handle(UpdateTaskStatusToCompleteCommand request, CancellationToken cancellationToken)
    {

        var task = 
            await _taskItemRepository
            .ChangeTaskStatusToCompletedAsync(request.Id, cancellationToken);


        return _mapper.Map<TaskResult>(task);
    }
}
