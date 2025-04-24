using Loovi.Test.Domain.Common.Interfaces;
using System;

namespace Loovi.Test.Domain.Common
{
    /// <summary>
    /// Abstract base class for all entity classes, providing a unique identifier and common functionality.
    /// </summary>
    public abstract class BaseEntity : IComparable<BaseEntity>, IEntity, IAuditable, ISoftDeletable
    {
        /// <summary>
        /// Unique identifier for the entity.
        /// </summary>
        public virtual Guid Id { get; set; }

        public virtual DateTime UpdatedAt { get; set; }
        public virtual DateTime CreatedAt { get; set; }

        public virtual bool Active { get; set; } = true;

        /// <summary>
        /// Compares the current entity with another entity based on their IDs.
        /// </summary>
        /// <param name="other">The other entity to compare to.</param>
        /// <returns>
        /// A value less than zero if this entity is less than the other entity,
        /// zero if they are equal, or a value greater than zero if this entity is greater.
        /// </returns>
        public virtual int CompareTo(BaseEntity? other)
        {
            if (other == null) return 1;
            else return Id.CompareTo(other.Id);
        }

        /// <summary>
        /// Activates the task.
        /// </summary>
        public virtual void Activate()
        {
            Active = true;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Deactivates (digital exclusion) the task.
        /// </summary>
        public virtual void Deactivate()
        {
            Active = false;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
