using AutoMapper;
using MediatR;
using FluentValidation;
using Loovi.Test.Domain.Entities;
using Loovi.Test.Domain.Repositories;
using Loovi.Test.Domain.Enums;
using Loovi.Test.Application.Tasks.Common;

namespace Loovi.Test.Application.Tasks.CreateTask
{
    /// <summary>
    /// Handler for processing CreateTaskCommand requests.
    /// </summary>
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, TaskResult>
    {
        private readonly ITaskItemRepository _taskRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of CreateTaskHandler.
        /// </summary>
        /// <param name="taskRepository">The task repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public CreateTaskHandler(ITaskItemRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the CreateTaskCommand request.
        /// </summary>
        /// <param name="command">The CreateTask command.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created task details.</returns>
        public async Task<TaskResult> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
        {
            var existingTask = await _taskRepository.ExistsByTitleAsync(command.Title, cancellationToken);
            if (existingTask)
                throw new InvalidOperationException($"Task with title '{command.Title}' already exists");


            var task = _mapper.Map<TaskItem>(command);

            task.UpdatedAt = task.CreatedAt = DateTime.UtcNow;
            task.Status = TaskItemStatus.Pending;

            var createdTask = await _taskRepository.CreateAsync(task, cancellationToken);

            return _mapper.Map<TaskResult>(createdTask);
        }
    }
}
