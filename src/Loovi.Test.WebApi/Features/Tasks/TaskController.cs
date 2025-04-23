using Loovi.Test.WebApi.Features.Tasks.CreateTask;
using Loovi.Test.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MediatR;
using Loovi.Test.WebApi.Common;
using Loovi.Test.Application.Tasks.CreateTask;
using Loovi.Test.Common.Responses;
using Loovi.Test.WebApi.Features.Tasks.Common;
using Loovi.Test.WebApi.Features.Tasks.UpdateTask;
using Loovi.Test.Application.Tasks.UpdateTask;

namespace Loovi.Test.WebApi.Controllers
{
    /// <summary>
    /// Controller for managing task operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of TaskController.
        /// </summary>
        /// <param name="mediator">The mediator instance.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public TaskController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
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


            return Created(response, "Task created successfully");
        }

        private IActionResult Created<T>(T data, string message)
        {
            var formattedResponse = ApiResponse<T>.Ok(data, message);

            return StatusCode(201, formattedResponse);
        }

        private IActionResult Ok<T>(T data, string message)
        {
            var formattedResponse = ApiResponse<T>.Ok(data, message);

            int httpCode = data == null ? 204 : 200;

            return StatusCode(httpCode, formattedResponse);
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
        //[HttpGet("{id}")]
        //[ProducesResponseType(typeof(ApiResponse<TaskResponse>), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ApiResponse<TaskResponse>), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(ApiResponse<TaskResponse>), StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> GetTask([FromRoute] Guid id, CancellationToken cancellationToken)
        //{
        //    var request = new GetTaskRequest { Id = id };

        //    var command = _mapper.Map<GetTaskCommand>(request.Id);
        //    var response = await _mediator.Send(command, cancellationToken);

        //    return Ok(_mapper.Map<GetTaskResponse>(response), "Task retrieved successfully");
        //}

        ///// <summary>
        ///// Retrieves a list of tasks based on query parameters.
        ///// </summary>
        ///// <param name="parameters">The query parameters.</param>
        ///// <param name="cancellationToken">Cancellation token.</param>
        ///// <returns>The list of tasks.</returns>
        //[HttpGet]
        //[ProducesResponseType(typeof(ApiResponseWithData<ListTasksResponse>), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> GetTasks(
        //    [FromQuery] IDictionary<string, string[]> parameters,
        //    CancellationToken cancellationToken)
        //{
        //    var command = new ListTasksCommand(parameters);
        //    var response = await _mediator.Send(command, cancellationToken);

        //    return Ok(
        //        _mapper.Map<ListTasksResponse>(response),
        //        "Tasks retrieved successfully");
        //}

        ///// <summary>
        ///// Deletes a task by its ID.
        ///// </summary>
        ///// <param name="id">The unique identifier of the task to delete.</param>
        ///// <param name="cancellationToken">Cancellation token.</param>
        ///// <returns>Success response if the task was deleted.</returns>
        //[HttpDelete("{id}")]
        //[ProducesResponseType(typeof(ApiResponseWithData<GetTaskResponse>), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> DeleteTask([FromRoute] Guid id, CancellationToken cancellationToken)
        //{
        //    var request = new DeleteTaskRequest { Id = id };
        //    var validator = new DeleteTaskRequestValidator();
        //    var validationResult = await validator.ValidateAsync(request, cancellationToken);

        //    if (!validationResult.IsValid)
        //        return BadRequest(validationResult.Errors);

        //    var command = _mapper.Map<DeleteTaskCommand>(request.Id);
        //    var task = await _mediator.Send(command, cancellationToken);

        //    return Ok("Task deleted successfully");
        //}
    }
}
