namespace Loovi.Test.WebApi.Features.Tasks.Common
{
    public class TaskResponse
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
        public TaskStatus Status { get; set; }
    }
}
