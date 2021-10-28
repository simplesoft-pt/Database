using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the read bulk operation by a collection of
    /// external unique identifiers
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The unique identifier type</typeparam>
    public class EFCoreReadByExternalIdRange<TEntity, TId> : IReadByExternalIdRange<TEntity, TId>
        where TEntity : class, IEntity, IHaveExternalId<TId>
        where TId : IEquatable<TId>
    {
        private readonly IQueryable<TEntity> _query;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="query"></param>
        public EFCoreReadByExternalIdRange(IQueryable<TEntity> query)
        {
            _query = query;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> ReadAsync(IEnumerable<TId> externalIds, CancellationToken ct)
        {
            if (externalIds == null) throw new ArgumentNullException(nameof(externalIds));

            return await _query.Where(e => externalIds.Contains(e.ExternalId)).ToListAsync(ct).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Represents the exists operation by an external unique identifier
    /// of <see cref="Guid"/> type.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public class EFCoreReadByExternalIdRange<TEntity> : EFCoreReadByExternalIdRange<TEntity, Guid>, IReadByExternalIdRange<TEntity>
        where TEntity : class, IEntity, IHaveExternalId
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="query"></param>
        public EFCoreReadByExternalIdRange(IQueryable<TEntity> query) : base(query)
        {

        }
    }
}
