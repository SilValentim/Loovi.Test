using Loovi.Test.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Application.Tasks.Common
{
    public class TaskResult
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
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date and time of the task was last updated.
        /// </summary>
        public DateTime UpdatedAt { get; set; }


        /// <summary>
        /// Deadline for completing the task.
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Current status of the task, indicating its progress.
        /// </summary>
        public TaskItemStatus Status { get; set; }

        /// <summary>
        /// Soft delete flag.
        /// </summary>
        public bool Active { get; set; }
    }
}
