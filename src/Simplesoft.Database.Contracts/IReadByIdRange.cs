using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the read operation by a unique identifier in bulk
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The unique identifier type</typeparam>
    public interface IReadByIdRange<TEntity, in TId> where TEntity : IEntity<TId> where TId : IEquatable<TId>
    {
        /// <summary>
        /// Reads a collection of entities by their unique identifiers.
        /// </summary>
        /// <param name="ids">The collection of ids</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The collection of entities</returns>
        Task<IEnumerable<TEntity>> ReadByIdAsync(IEnumerable<TId> ids, CancellationToken ct);

        /// <summary>
        /// Reads a collection of entities by their unique identifiers.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <param name="ids">The collection of ids</param>
        /// <returns>The collection of entities</returns>
        Task<IEnumerable<TEntity>> ReadByIdAsync(CancellationToken ct, params TId[] ids);
    }

    /// <summary>
    /// Represents the read operation by a long unique identifier in bulk
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface IReadByIdRange<TEntity> where TEntity : IEntity<long>
    {
        /// <summary>
        /// Reads a collection of entities by their unique identifiers.
        /// </summary>
        /// <param name="ids">The collection of ids</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The collection of entities</returns>
        Task<IEnumerable<TEntity>> ReadByIdAsync(IEnumerable<long> ids, CancellationToken ct);

        /// <summary>
        /// Reads a collection of entities by their unique identifiers.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <param name="ids">The collection of ids</param>
        /// <returns>The collection of entities</returns>
        Task<IEnumerable<TEntity>> ReadByIdAsync(CancellationToken ct, params long[] ids);
    }
}