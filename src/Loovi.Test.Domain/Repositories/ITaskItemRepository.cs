using Loovi.Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Domain.Repositories
{
    public interface ITaskItemRepository : IBaseRepository<TaskItem>
    {
        Task<bool> ExistsByTitleAsync(string title, CancellationToken cancellationToken = default);
        Task<bool> ExistsSameTitleAndDifferentIdAsync(TaskItem task, CancellationToken cancellationToken = default);
    }
}
