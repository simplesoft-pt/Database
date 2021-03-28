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
    /// <typeparam name="TContext">The context type</typeparam>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The unique identifier type</typeparam>
    public class EFCoreReadByExternalIdRange<TContext, TEntity, TId> : IReadByExternalIdRange<TEntity, TId>
        where TContext : DbContext
        where TEntity : class, IEntity, IHaveExternalId<TId>
        where TId : IEquatable<TId>
    {
        private readonly IQueryable<TEntity> _query;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="context"></param>
        public EFCoreReadByExternalIdRange(
            TContext context
        )
        {
            _query = context.Set<TEntity>().AsNoTracking();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> ReadAsync(IEnumerable<TId> externalIds, CancellationToken ct)
        {
            if (externalIds == null) throw new ArgumentNullException(nameof(externalIds));

            return await _query.Where(e => externalIds.Contains(e.ExternalId)).ToListAsync(ct);
        }
    }
}
