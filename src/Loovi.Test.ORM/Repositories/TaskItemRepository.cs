using Loovi.Test.Domain.Common;
using Loovi.Test.Domain.Entities;
using Loovi.Test.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.ORM.Repositories
{
    class TaskItemRepository : BaseRepository<TaskItem>, ITaskItemRepository
    {
        public TaskItemRepository(MainContext context) : base(context)
        {
        }

        /// <summary>
        /// Checks if a TaskItem with the specified title exists.
        /// </summary>
        /// <param name="title">The title of the task.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if a TaskItem with the specified title exists; otherwise, false.</returns>
        public async Task<bool> ExistsByTitleAsync(string title, CancellationToken cancellationToken = default)
        {
            return await _context.Tasks
                .AnyAsync(task => task.Title == title, cancellationToken);
        }

        public async Task<bool> ExistsSameTitleAndDifferentIdAsync(TaskItem task, CancellationToken cancellationToken = default)
        {
            return await _context.Tasks
                .AnyAsync(t => t.Id != task.Id && t.Title == task.Title, cancellationToken);
        }

        public override async Task<TaskItem> UpdateAsync(TaskItem updatedEntity, CancellationToken cancellationToken = default)
        {
            var currentlyEntity = await GetByIdAsync(updatedEntity.Id, cancellationToken);

            updatedEntity.Status = currentlyEntity.Status;
            updatedEntity.CreatedAt = currentlyEntity.CreatedAt;
            updatedEntity.Active = currentlyEntity.Active;
            updatedEntity.UpdatedAt = DateTime.UtcNow;

            if (currentlyEntity != null)
            {
                _context.Entry(currentlyEntity).CurrentValues.SetValues(updatedEntity);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return updatedEntity;
        }

        public async Task<Paginated<TaskItem>> GetTasksAsync(IDictionary<string, string[]> filters,
            CancellationToken cancellationToken = default)
        {
            var result = await GetList(filters, cancellationToken);
            return result;
        }
    }
}
