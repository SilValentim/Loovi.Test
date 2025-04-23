using AutoMapper;
using MediatR;
using FluentValidation;
using Loovi.Test.Domain.Entities;
using Loovi.Test.Domain.Repositories;
using Loovi.Test.Domain.Enums;
using Loovi.Test.Application.Tasks.Common;

namespace Loovi.Test.Application.Tasks.UpdateTask
{
    /// <summary>
    /// Handler for processing UpdateTaskCommand requests.
    /// </summary>
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, TaskResult>
    {
        private readonly ITaskItemRepository _taskRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of UpdateTaskHandler.
        /// </summary>
        /// <param name="taskRepository">The task repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public UpdateTaskHandler(ITaskItemRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the UpdateTaskCommand request.
        /// </summary>
        /// <param name="command">The UpdateTask command.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated task details.</returns>
        public async Task<TaskResult> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
        {
            var task = _mapper.Map<TaskItem>(command);

            var existingTask = await _taskRepository
                .ExistsSameTitleAndDifferentIdAsync(task, cancellationToken);

            if (existingTask)
                throw new InvalidOperationException($"Task with title '{command.Title}' already exists");

            var createdTask = await _taskRepository.UpdateAsync(task, cancellationToken);

            return _mapper.Map<TaskResult>(createdTask);
        }
    }
}
