using System;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents an entity with an unique identifier to be used
    /// across multiple systems
    /// </summary>
    /// <typeparam name="TId">The external id type</typeparam>
    public interface IHaveExternalId<TId> where TId : IEquatable<TId>
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        TId ExternalId { get; set; }
    }

    /// <summary>
    /// Represents an entity with an unique identifier to be used
    /// across multiple systems, with a <see cref="Guid"/> for the
    /// <see cref="IHaveExternalId{TId}.ExternalId"/> property.
    /// </summary>
    public interface IHaveExternalId : IHaveExternalId<Guid>
    {

    }
}
