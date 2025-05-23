﻿using Loovi.Test.Domain.Common;
using Microsoft.EntityFrameworkCore;
using LinqKit;
using System.Reflection;
using Loovi.Test.Domain.Repositories;
using Loovi.Test.Domain.Common.Interfaces;
using Loovi.Test.Common.Auth.Interfaces;
using System.Threading;

namespace Loovi.Test.ORM.Repositories
{
    /// <summary>
    /// Base repository class providing common data access methods for entities.
    /// </summary>
    /// <typeparam name="Entity">The type of the entity that inherits from <see cref="BaseEntity"/>.</typeparam>
    public class BaseRepository<Entity> : IBaseRepository<Entity> where Entity : BaseEntity
    {
        protected readonly MainContext _context;
        protected readonly IUserAccessor _userAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{Entity}"/> class.
        /// </summary>
        /// <param name="context">The database context to be used by the repository.</param>
        public BaseRepository(MainContext context, IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }

        /// <summary>
        /// Retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>The entity if found; otherwise, null.</returns>
        public virtual async Task<Entity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        { 
            if (typeof(IUserIdentity).IsAssignableFrom(typeof(Entity)))
            {
                var userId = _userAccessor.GetUserId();

                return await _context.Set<Entity>()
                    .FirstOrDefaultAsync(
                        e => e.Id.CompareTo(id) == 0 &&
                             ((IUserIdentity)e).UserId == userId &&
                             e.Active,
                        cancellationToken);
            }
            else
            {
                return await _context.Set<Entity>()
                    .FirstOrDefaultAsync(
                        e => e.Id.CompareTo(id) == 0 && e.Active, cancellationToken);
            }
        }

        /// <summary>
        /// Creates a new entity in the database.
        /// </summary>
        /// <param name="entity">The entity to be created.</param>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>The created entity.</returns>
        public virtual async Task<Entity> CreateAsync(Entity entity, CancellationToken cancellationToken = default)
        {
            entity.Id = Guid.NewGuid();

            if (entity is IUserIdentity userOwnedEntity)
            {
                userOwnedEntity.UserId = _userAccessor.GetUserId();
            }

            await _context.Set<Entity>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        /// <summary>
        /// Updates an existing entity in the database.
        /// </summary>
        /// <param name="updatedEntity">The entity with updated values.</param>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>The updated entity.</returns>
        public virtual async Task<Entity> UpdateAsync(Entity updatedEntity, CancellationToken cancellationToken = default)
        {
            var currentlyEntity = await GetByIdAsync(updatedEntity.Id, cancellationToken);
            updatedEntity.CreatedAt = currentlyEntity.CreatedAt;
            updatedEntity.Active = currentlyEntity.Active;
            updatedEntity.UpdatedAt = DateTime.UtcNow;

            if (updatedEntity is IUserIdentity ownedEntity)
            {
                var currentUserId = _userAccessor.GetUserId();
                if (ownedEntity.UserId != currentUserId)
                {
                    //TODO Add log of possible security attack.
                    throw new UnauthorizedAccessException("Cannot modify entity owned by another user.");
                }
            }

            if (currentlyEntity != null)
            {
                _context.Entry(currentlyEntity).CurrentValues.SetValues(updatedEntity);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return updatedEntity;
        }

        /// <summary>
        /// Deletes a entity from the database
        /// </summary>
        /// <param name="id">The unique identifier of the entity to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The deleted entity</returns>
        public async Task<Entity> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);

            if (entity != null)
            {
                entity.Deactivate();
                await _context.SaveChangesAsync(cancellationToken);
            }
            return entity;
        }

        /// <summary>
        /// Retrieves a paginated list of entities based on filters and ordering.
        /// </summary>
        /// <param name="filters">A dictionary of filters to apply to the query.</param>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>A paginated list of entities.</returns>
        protected async Task<Paginated<Entity>> GetList(
            IDictionary<string, string[]> filters,
            CancellationToken cancellationToken)
        {
            InitializePaginationAndOrdering(
                out int page,
                out int size,
                out string order,
                ref filters);

            var entityQueryable = _context.Set<Entity>().AsQueryable();
            
            var expressionQueryable = Filter(filters, entityQueryable);

            expressionQueryable = Ordering(order, expressionQueryable);

            var result = await ExecutePaginatedQuery(page, size, expressionQueryable, cancellationToken);

            return result;
        }

        /// <summary>
        /// Checks if an entity exists in the database by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <returns>True if the entity exists; otherwise, false.</returns>
        public async Task<bool> ExistsAsync(Guid id)
        {
            if (typeof(IUserIdentity).IsAssignableFrom(typeof(Entity)))
            {
                var userId = _userAccessor.GetUserId();

                return await _context
                    .Set<Entity>()
                    .AnyAsync(u => 
                        u.Id.CompareTo(id) == 0 &&
                        ((IUserIdentity)u).UserId == userId);
            }
            else
            {
                return await _context
                    .Set<Entity>()
                    .AnyAsync(u => u.Id.CompareTo(id) == 0);
            }
            
        }

        #region List Methods

        /// <summary>
        /// Initializes pagination and ordering parameters from the provided filters.
        /// </summary>
        /// <param name="page">The current page number.</param>
        /// <param name="size">The number of items per page.</param>
        /// <param name="order">The ordering string.</param>
        /// <param name="filters">The dictionary of filters to modify.</param>
        protected void InitializePaginationAndOrdering(
            out int page, out int size, out string order,
            ref IDictionary<string, string[]> filters)
        {
            page = filters.ContainsKey("_page") ? int.Parse(filters["_page"][0]) : 1;
            size = filters.ContainsKey("_size") ? int.Parse(filters["_size"][0]) : 10;
            order = filters.ContainsKey("_order") ? filters["_order"][0] : string.Empty;

            filters.Remove("_page");
            filters.Remove("_size");
            filters.Remove("_order");
        }

        /// <summary>
        /// Applies filters to the queryable entity set.
        /// </summary>
        /// <param name="filters">The dictionary of filters to apply.</param>
        /// <param name="entityQueryable">The queryable entity set.</param>
        /// <returns>The filtered queryable entity set.</returns>
        protected IQueryable<Entity> Filter(
            IDictionary<string, string[]> filters, IQueryable<Entity> entityQueryable)
        {
            var expressionFilter = PredicateBuilder.New<Entity>(true);

            expressionFilter = expressionFilter.And(x => x.Active);
            
            if (typeof(IUserIdentity).IsAssignableFrom(typeof(Entity)))
            {
                expressionFilter = expressionFilter.And(x =>
                    ((IUserIdentity)x).UserId == _userAccessor.GetUserId());
            }

            foreach (var filter in filters)
            {
                var propertyName = filter.Key;
                var values = filter.Value;

                var property = typeof(Entity).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (property != null)
                {
                    var innerPredicate = PredicateBuilder.New<Entity>(false);

                    foreach (var value in values)
                    {
                        if (property.PropertyType == typeof(string))
                        {
                            if (value.StartsWith("*") && value.EndsWith("*"))
                            {
                                var searchValue = value.Trim('*');
                                innerPredicate = innerPredicate.Or(p => EF.Property<string>(p, property.Name).Contains(searchValue));
                            }
                            else if (value.StartsWith("*"))
                            {
                                var searchValue = value.TrimStart('*');
                                innerPredicate = innerPredicate.Or(p => EF.Property<string>(p, property.Name).EndsWith(searchValue));
                            }
                            else if (value.EndsWith("*"))
                            {
                                var searchValue = value.TrimEnd('*');
                                innerPredicate = innerPredicate.Or(p => EF.Property<string>(p, property.Name).StartsWith(searchValue));
                            }
                            else
                            {
                                innerPredicate = innerPredicate.Or(p => EF.Property<string>(p, property.Name) == value);
                            }
                        }
                        else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                        {
                            if (value.StartsWith(">="))
                            {
                                var val = DateTime.Parse(value[2..]);
                                innerPredicate = innerPredicate.Or(p => EF.Property<DateTime>(p, property.Name) >= val);
                            }
                            else if (value.StartsWith("<="))
                            {
                                var val = DateTime.Parse(value[2..]);
                                innerPredicate = innerPredicate.Or(p => EF.Property<DateTime>(p, property.Name) <= val);
                            }
                            else if (value.StartsWith(">"))
                            {
                                var val = DateTime.Parse(value[1..]);
                                innerPredicate = innerPredicate.Or(p => EF.Property<DateTime>(p, property.Name) > val);
                            }
                            else if (value.StartsWith("<"))
                            {
                                var val = DateTime.Parse(value[1..]);
                                innerPredicate = innerPredicate.Or(p => EF.Property<DateTime>(p, property.Name) < val);
                            }
                            else
                            {
                                var val = DateTime.Parse(value);
                                innerPredicate = innerPredicate.Or(p => EF.Property<DateTime>(p, property.Name) == val);
                            }
                        }
                        else
                        {
                            var typedValue = Convert.ChangeType(value, property.PropertyType);
                            innerPredicate = innerPredicate.Or(p => EF.Property<object>(p, property.Name).Equals(typedValue));
                        }
                    }

                    expressionFilter = expressionFilter.And(innerPredicate);
                }
            }

            entityQueryable = entityQueryable.AsExpandable().Where(expressionFilter);

            return entityQueryable;
        }

        /// <summary>
        /// Applies ordering to the queryable entity set.
        /// </summary>
        /// <param name="order">The ordering string.</param>
        /// <param name="entityQueryable">The queryable entity set.</param>
        /// <returns>The ordered queryable entity set.</returns>
        protected IQueryable<Entity> Ordering(string order,
            IQueryable<Entity> entityQueryable)
        {
            if (!string.IsNullOrEmpty(order))
            {
                var orderParams = order.Split(',');
                bool isFirstOrder = true;

                foreach (var param in orderParams)
                {
                    var trimmedParam = param.Trim();
                    var descending = trimmedParam.EndsWith(" desc", StringComparison.OrdinalIgnoreCase);
                    var propertyName = trimmedParam.Replace(" desc", "", StringComparison.OrdinalIgnoreCase)
                                                   .Replace(" asc", "", StringComparison.OrdinalIgnoreCase);

                    var property = typeof(Entity).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (property != null)
                    {
                        entityQueryable = isFirstOrder
                            ? (descending
                                ? entityQueryable.OrderByDescending(p => EF.Property<object>(p, property.Name))
                                : entityQueryable.OrderBy(p => EF.Property<object>(p, property.Name)))
                            : (descending
                                ? ((IOrderedQueryable<Entity>)entityQueryable).ThenByDescending(p => EF.Property<object>(p, property.Name))
                                : ((IOrderedQueryable<Entity>)entityQueryable).ThenBy(p => EF.Property<object>(p, property.Name)));

                        isFirstOrder = false;
                    }
                }
            }

            return entityQueryable;
        }

        /// <summary>
        /// Executes a paginated query on the entity set.
        /// </summary>
        /// <param name="page">The current page number.</param>
        /// <param name="size">The number of items per page.</param>
        /// <param name="entityQueryable">The queryable entity set.</param>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>A paginated result containing the entities.</returns>
        protected async Task<Paginated<Entity>> ExecutePaginatedQuery(
            int page,
            int size,
            IQueryable<Entity> entityQueryable,
            CancellationToken cancellationToken)
        {
            var total = await entityQueryable.CountAsync(cancellationToken);

            var totalPages = (int)Math.Ceiling((total / (double)size));
            var paginatedProducts = await
                 entityQueryable
                .Skip(size * (page - 1))
                .Take(size)
                .ToListAsync(cancellationToken);

            var result = new Paginated<Entity>
            {
                Items = paginatedProducts,
                TotalItems = total,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return result;
        }

        #endregion
    }
}
