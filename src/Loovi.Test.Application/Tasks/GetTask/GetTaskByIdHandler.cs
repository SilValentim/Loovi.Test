using AutoMapper;
using MediatR;
using FluentValidation;
using Loovi.Test.Domain.Entities;
using Loovi.Test.Domain.Repositories;
using Loovi.Test.Domain.Enums;
using Loovi.Test.Application.Tasks.Common;

namespace Loovi.Test.Application.Tasks.GetTask
{
    /// <summary>
    /// Handler for processing GetTaskCommand requests.
    /// </summary>
    public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, TaskResult>
    {
        private readonly ITaskItemRepository _taskRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of GetTaskHandler.
        /// </summary>
        /// <param name="taskRepository">The task repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public GetTaskByIdHandler(ITaskItemRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the GetTaskCommand request.
        /// </summary>
        /// <param name="command">The GetTask command.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated task details.</returns>
        public async Task<TaskResult> Handle(GetTaskByIdQuery command, CancellationToken cancellationToken)
        {
            var existingTask = await _taskRepository.GetByIdAsync(command.Id, cancellationToken);

            if (existingTask == null)
                throw new KeyNotFoundException($"Task with ID {command.Id} not found");


            return _mapper.Map<TaskResult>(existingTask);
        }
    }
}
