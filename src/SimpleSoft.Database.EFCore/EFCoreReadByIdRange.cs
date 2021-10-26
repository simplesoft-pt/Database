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
    /// unique identifiers
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The unique identifier type</typeparam>
    public class EFCoreReadByIdRange<TEntity, TId> : IReadByIdRange<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId : IEquatable<TId>
    {
        private readonly IQueryable<TEntity> _query;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="query"></param>
        public EFCoreReadByIdRange(IQueryable<TEntity> query)
        {
            _query = query;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> ReadAsync(IEnumerable<TId> ids, CancellationToken ct)
        {
            if (ids == null) throw new ArgumentNullException(nameof(ids));

            return await _query.Where(e => ids.Contains(e.Id)).ToListAsync(ct);
        }
    }

    /// <summary>
    /// Represents the read bulk operation by a collection of
    /// unique identifiers of <see cref="long"/> type.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public class EFCoreReadByIdRange<TEntity> : EFCoreReadByIdRange<TEntity, long>, IReadByIdRange<TEntity>
        where TEntity : class, IEntity<long>
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="query"></param>
        public EFCoreReadByIdRange(IQueryable<TEntity> query) : base(query)
        {

        }
    }
}
