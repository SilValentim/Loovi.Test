using AutoMapper;
using MediatR;
using FluentValidation;
using Loovi.Test.Domain.Entities;
using Loovi.Test.Domain.Repositories;
using Loovi.Test.Domain.Enums;
using Loovi.Test.Application.Tasks.Common;

namespace Loovi.Test.Application.Tasks.DeleteTask
{
    /// <summary>
    /// Handler for processing DeleteTaskCommand requests.
    /// </summary>
    public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, TaskResult>
    {
        private readonly ITaskItemRepository _taskRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of DeleteTaskHandler.
        /// </summary>
        /// <param name="taskRepository">The task repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public DeleteTaskHandler(ITaskItemRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the DeleteTaskCommand request.
        /// </summary>
        /// <param name="command">The DeleteTask command.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created task details.</returns>
        public async Task<TaskResult> Handle(DeleteTaskCommand command, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.DeleteAsync(command.Id, cancellationToken);
            if (task == null)
                throw new KeyNotFoundException($"Task with ID {command.Id} not found");

            return _mapper.Map<TaskResult>(task);
        }
    }
}
