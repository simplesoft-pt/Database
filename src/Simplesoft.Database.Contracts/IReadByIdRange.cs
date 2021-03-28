using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the read bulk operation by a collection of
    /// unique identifiers
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The unique identifier type</typeparam>
    public interface IReadByIdRange<TEntity, in TId> 
        where TEntity : class, IEntity<TId> 
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// Reads a collection of entities by their unique identifiers.
        /// </summary>
        /// <param name="ids">The collection of ids</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The collection of entities</returns>
        Task<IEnumerable<TEntity>> ReadAsync(IEnumerable<TId> ids, CancellationToken ct);
    }

    /// <summary>
    /// Represents the read bulk operation by a collection of
    /// unique identifiers of <see cref="long"/> type.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface IReadByIdRange<TEntity> : IReadByIdRange<TEntity, long>
        where TEntity : class, IEntity<long>
    {

    }
}