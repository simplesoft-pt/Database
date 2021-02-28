using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the exists operation by an unique identifier.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The unique identifier type</typeparam>
    public interface IExistsById<TEntity, in TId>
        where TEntity : class, IEntity<TId>
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// Checks if an entity exists for a given unique identifier
        /// </summary>
        /// <param name="id">The entity id</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>True if exists, otherwise false</returns>
        Task<bool> ExistsAsync(TId id, CancellationToken ct);
    }

    /// <summary>
    /// Represents the exists operation by an unique identifier
    /// of <see cref="long"/> type.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface IExistsById<TEntity> : IExistsById<TEntity, long>
        where TEntity : class, IEntity<long>
    {

    }
}
