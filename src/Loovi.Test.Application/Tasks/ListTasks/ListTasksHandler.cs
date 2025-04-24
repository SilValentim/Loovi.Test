using AutoMapper;
using MediatR;
using FluentValidation;
using Loovi.Test.Domain.Entities;
using Loovi.Test.Domain.Repositories;
using Loovi.Test.Domain.Enums;
using Loovi.Test.Application.Tasks.Common;
using Loovi.Test.Domain.Common;
using Loovi.Test.Application.Common;

namespace Loovi.Test.Application.Tasks.ListTasks
{
    /// <summary>
    /// Handler for processing ListTasksCommand requests.
    /// </summary>
    public class ListTasksHandler : IRequestHandler<ListTasksCommand, PaginatedResult<TaskResult>>
    {
        private readonly ITaskItemRepository _taskRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of ListTasksHandler.
        /// </summary>
        /// <param name="taskRepository">The task repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public ListTasksHandler(ITaskItemRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the ListTasksCommand request.
        /// </summary>
        /// <param name="command">The ListTasks command.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated task details.</returns>
        public async Task<PaginatedResult<TaskResult>> Handle(ListTasksCommand command, CancellationToken cancellationToken)
        {
            var listProducts = await _taskRepository.GetTasksAsync(
             command.Parameters, cancellationToken);

            var result = _mapper.Map<PaginatedResult<TaskResult>>(listProducts);

            return result;
        }
    }
}
