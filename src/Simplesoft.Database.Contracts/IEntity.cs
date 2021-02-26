using System;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents a database entity with the property
    /// <see cref="Id"/> representing its unique identifier
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public interface IEntity<TId> where TId : IEquatable<TId>
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        TId Id { get; set; }
    }

    /// <summary>
    /// Represents a database entity with a long
    /// representing the unique identifier
    /// </summary>
    public interface IEntity : IEntity<long>
    {

    }
}
