using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the read bulk operation by a collection of
    /// external unique identifiers
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The unique identifier type</typeparam>
    public interface IReadByExternalIdRange<TEntity, in TId> 
        where TEntity : class, IEntity, IHaveExternalId<TId>
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// Reads a collection of entities by their external unique identifiers.
        /// </summary>
        /// <param name="ids">The collection of ids</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The collection of entities</returns>
        Task<IEnumerable<TEntity>> ReadAsync(IEnumerable<TId> ids, CancellationToken ct);

        /// <summary>
        /// Reads a collection of entities by their external unique identifiers.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <param name="ids">The collection of ids</param>
        /// <returns>The collection of entities</returns>
        Task<IEnumerable<TEntity>> ReadAsync(CancellationToken ct, params TId[] ids);
    }

    /// <summary>
    /// Represents the read bulk operation by a collection of
    /// external unique identifiers of <see cref="Guid"/> type.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface IReadByExternalIdRange<TEntity> : IReadByExternalIdRange<TEntity, Guid>
        where TEntity : class, IEntity, IHaveExternalId
    {

    }
}