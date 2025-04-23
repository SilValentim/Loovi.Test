using System;

namespace Loovi.Test.WebApi.Features.Tasks.UpdateTask
{
    /// <summary>
    /// Represents the request model for creating a new task.
    /// </summary>
    public class UpdateTaskRequest
    {
        /// <summary>
        /// Identifier of the task.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Title of the task.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Detailed description of the task.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Deadline for completing the task.
        /// </summary>
        public DateTime DueDate { get; set; }
    }
}
