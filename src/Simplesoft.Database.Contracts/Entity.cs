using System;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents a database entity with the property
    /// <see cref="Id"/> representing its unique identifier
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public abstract class Entity<TId> : IEntity<TId> where TId : IEquatable<TId>
    {
        /// <inheritdoc />
        public virtual TId Id { get; set; }
    }

    /// <summary>
    /// Represents a database entity with a long
    /// representing the unique identifier
    /// </summary>
    public abstract class Entity : IEntity
    {
        /// <inheritdoc />
        public virtual long Id { get; set; }
    }
}
