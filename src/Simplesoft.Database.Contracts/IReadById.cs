using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the read operation by a unique identifier
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The unique identifier type</typeparam>
    public interface IReadById<TEntity, in TId> 
        where TEntity : IEntity<TId> 
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// Read an entity by a unique identifier, returning null if not found.
        /// </summary>
        /// <param name="id">The entity id</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The entity or null if not found</returns>
        Task<TEntity> ReadByIdAsync(TId id, CancellationToken ct);
    }

    /// <summary>
    /// Represents the read operation by a long unique identifier
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface IReadById<TEntity> : IReadById<TEntity, long>
        where TEntity : class, IEntity<long>
    {

    }
}
