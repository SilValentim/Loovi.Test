using System;

namespace Loovi.Test.Domain.Common
{
    /// <summary>
    /// Abstract base class for all entity classes, providing a unique identifier and common functionality.
    /// </summary>
    public abstract class BaseEntity : IComparable<BaseEntity>
    {
        /// <summary>
        /// Unique identifier for the entity.
        /// </summary>
        public virtual Guid Id { get; set; }

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
    }
}
