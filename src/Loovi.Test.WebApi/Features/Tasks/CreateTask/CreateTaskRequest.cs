using System;

namespace Loovi.Test.WebApi.Features.Tasks.CreateTask
{
    /// <summary>
    /// Represents the request model for creating a new task.
    /// </summary>
    public class CreateTaskRequest
    {
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
