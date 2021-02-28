using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the read operation by an external unique identifier
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The unique identifier type</typeparam>
    public interface IReadByExternalId<TEntity, in TId> 
        where TEntity : IEntity, IHaveExternalId<TId> 
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// Read an entity by a unique identifier, returning null if not found.
        /// </summary>
        /// <param name="id">The entity id</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The entity or null if not found</returns>
        Task<TEntity> ReadByExternalIdAsync(TId id, CancellationToken ct);
    }

    /// <summary>
    /// Represents the read operation by an external unique identifier
    /// of <see cref="Guid"/> type.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface IReadByExternalId<TEntity> : IReadByExternalId<TEntity, Guid>
        where TEntity : IEntity, IHaveExternalId
    {

    }
}
