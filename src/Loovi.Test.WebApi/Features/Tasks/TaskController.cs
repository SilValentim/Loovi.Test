
using Loovi.Test.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MediatR;
using Loovi.Test.Application.Tasks.CreateTask;
using Loovi.Test.Common.Responses;
using Loovi.Test.WebApi.Features.Tasks.Common;
using Loovi.Test.WebApi.Features.Tasks.UpdateTask;
using Loovi.Test.Application.Tasks.UpdateTask;
using Loovi.Test.Application.Tasks.GetTask;
using Loovi.Test.WebApi.Features.Tasks.CreateTask;
using Loovi.Test.WebApi.Common;
using Loovi.Test.Application.Tasks.ListTasks;
using Loovi.Test.Application.Tasks.DeleteTask;
using Microsoft.AspNetCore.Authorization;

namespace Loovi.Test.WebApi.Controllers
{
    /// <summary>
    /// Controller for managing task operations.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : BaseController
    {        /// <summary>
        /// Initializes a new instance of TaskController.
        /// </summary>
        /// <param name="mediator">The mediator instance.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public TaskController(IMediator mediator, IMapper mapper)
            :base (mediator, mapper)
        {
        }

        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <param name="request">The task creation request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created task details.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<TaskResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<TaskResponse>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateTaskCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);


            return Created(_mapper.Map<TaskResponse>(response), "Task created successfully");
        }

        /// <summary>
        /// Updates an existing task.
        /// </summary>
        /// <param name="request">The task update request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated task details.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<TaskResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<TaskResponse>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskRequest request, CancellationToken cancellationToken)
        {

            var command = _mapper.Map<UpdateTaskCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(_mapper.Map<TaskResponse>(response), "Task updated successfully");
        }

        /// <summary>
        /// Retrieves a task by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the task.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The task details if found.</returns>
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(ApiResponse<TaskResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<TaskResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<TaskResponse>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTask([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<GetTaskCommand>(id);
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(_mapper.Map<TaskResponse>(response), "Task retrieved successfully");
        }

        ///// <summary>
        ///// Retrieves a list of tasks based on query parameters.
        ///// </summary>
        ///// <param name="parameters">The query parameters.</param>
        ///// <param name="cancellationToken">Cancellation token.</param>
        ///// <returns>The list of tasks.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PaginatedResponse<TaskResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PaginatedResponse<TaskResponse>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTasks(
            [FromQuery] IDictionary<string, string[]> parameters,
            CancellationToken cancellationToken)
        {
            var command = new ListTasksCommand(parameters);
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(
                _mapper.Map<PaginatedResponse<TaskResponse>>(response),
                "Tasks retrieved successfully");
        }

        /// <summary>
        /// Deletes a task by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the task to delete.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Success response if the task was deleted.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<TaskResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<TaskResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<TaskResponse>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTask([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<DeleteTaskCommand>(id);
            var task = await _mediator.Send(command, cancellationToken);

            return Ok(_mapper.Map<TaskResponse>(task),"Task deleted successfully");
        }
    }
}
