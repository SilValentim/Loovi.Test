using Loovi.Test.WebApi.Features.Tasks.Enums;
using System;

namespace Loovi.Test.WebApi.Features.Tasks.CreateTask
{
    /// <summary>
    /// Represents the request model for creating a new task.
    /// </summary>
    public class CreateTaskResponse
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
        /// Date and time when the task was created.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Deadline for completing the task.
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Current status of the task, indicating its progress.
        /// </summary>
        public TaskItemStatus Status { get; set; }
    }
}
