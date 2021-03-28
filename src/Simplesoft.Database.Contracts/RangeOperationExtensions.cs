using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Extension methods for interfaces representing bulk operations
    /// </summary>
    public static class RangeOperationExtensions
    {
        /// <summary>
        /// Creates a range of entities
        /// </summary>
        /// <param name="createRange"></param>
        /// <param name="ct">The cancellation token</param>
        /// <param name="entities">The entity collection</param>
        /// <returns>The entity collection after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Task<IEnumerable<TEntity>> CreateAsync<TEntity>(
            this ICreateRange<TEntity> createRange,
            CancellationToken ct,
            params TEntity[] entities
        ) where TEntity : class, IEntity
        {
            if (createRange == null) throw new ArgumentNullException(nameof(createRange));

            return createRange.CreateAsync(entities, ct);
        }

        /// <summary>
        /// Reads a collection of entities by their external unique identifiers.
        /// </summary>
        /// <param name="readByExternalIdRange"></param>
        /// <param name="ct">The cancellation token</param>
        /// <param name="externalIds">The collection of external unique identifiers</param>
        /// <returns>The collection of entities</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Task<IEnumerable<TEntity>> ReadAsync<TEntity, TId>(
            this IReadByExternalIdRange<TEntity, TId> readByExternalIdRange,
            CancellationToken ct,
            params TId[] externalIds
        )
            where TEntity : class, IEntity, IHaveExternalId<TId>
            where TId : IEquatable<TId>
        {
            if (readByExternalIdRange == null) throw new ArgumentNullException(nameof(readByExternalIdRange));

            return readByExternalIdRange.ReadAsync(externalIds, ct);
        }

        /// <summary>
        /// Reads a collection of entities by their unique identifiers.
        /// </summary>
        /// <param name="readByIdRange"></param>
        /// <param name="ct">The cancellation token</param>
        /// <param name="ids">The collection of ids</param>
        /// <returns>The collection of entities</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Task<IEnumerable<TEntity>> ReadAsync<TEntity, TId>(
            this IReadByIdRange<TEntity, TId> readByIdRange,
            CancellationToken ct,
            params TId[] ids
        )
            where TEntity : class, IEntity<TId>
            where TId : IEquatable<TId>
        {
            if (readByIdRange == null) throw new ArgumentNullException(nameof(readByIdRange));

            return readByIdRange.ReadAsync(ids, ct);
        }

        /// <summary>
        /// Updates a range of entities
        /// </summary>
        /// <param name="updateRange"></param>
        /// <param name="ct">The cancellation token</param>
        /// <param name="entities">The entity collection</param>
        /// <returns>The entity collection after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Task<IEnumerable<TEntity>> UpdateAsync<TEntity>(
            this IUpdateRange<TEntity> updateRange,
            CancellationToken ct,
            params TEntity[] entities
        ) where TEntity : class, IEntity
        {
            if (updateRange == null) throw new ArgumentNullException(nameof(updateRange));

            return updateRange.UpdateAsync(entities, ct);
        }

        /// <summary>
        /// Deletes a range of entities
        /// </summary>
        /// <param name="deleteRange"></param>
        /// <param name="ct">The cancellation token</param>
        /// <param name="entities">The entity collection</param>
        /// <returns>The entity collection after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Task<IEnumerable<TEntity>> DeleteAsync<TEntity>(
            this IDeleteRange<TEntity> deleteRange,
            CancellationToken ct,
            params TEntity[] entities
        ) where TEntity : class, IEntity
        {
            if (deleteRange == null) throw new ArgumentNullException(nameof(deleteRange));

            return deleteRange.DeleteAsync(entities, ct);
        }
    }
}
