using Loovi.Test.Domain.Common;
using Loovi.Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Domain.Repositories
{
    public interface IBaseRepository<Entity> where Entity : BaseEntity
    {
        //Task<Paginated<Entity>> GetList(
        //    IDictionary<string, string[]> filters,
        //    CancellationToken cancellationToken);

        Task<Entity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Entity> CreateAsync(Entity entity, CancellationToken cancellationToken = default);
        Task<Entity> UpdateAsync(Entity updatedEntity, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid Id);

    }
}
