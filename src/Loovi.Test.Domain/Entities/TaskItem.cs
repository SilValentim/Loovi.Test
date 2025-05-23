using System;
using Loovi.Test.Domain.Common;
using Loovi.Test.Domain.Enums;

namespace Loovi.Test.Domain.Entities
{
    /// <summary>
        /// Represents a task with basic information, including title, description, dates, and status.
        /// </summary>
        public class TaskItem : UserOwnedEntity
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

            /// <summary>
            /// Current status of the task, indicating its progress.
            /// </summary>
            public TaskItemStatus Status { get; set; }

        public void ChangeTaskStatusToInProgress()
        {
            Status = TaskItemStatus.InProgress;
        }

        public void ChangeTaskStatusToCompleted()
        {
            Status = TaskItemStatus.Completed;
        }
    }

}
