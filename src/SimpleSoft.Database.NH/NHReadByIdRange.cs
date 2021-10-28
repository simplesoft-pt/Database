using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class NHReadByIdRange<TEntity, TId> : IReadByIdRange<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId : IEquatable<TId>
    {
        private readonly IQueryable<TEntity> _query;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="query"></param>
        public NHReadByIdRange(IQueryable<TEntity> query)
        {
            _query = query;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> ReadAsync(IEnumerable<TId> ids, CancellationToken ct)
        {
            if (ids == null) throw new ArgumentNullException(nameof(ids));

            return await _query.Where(e => ids.Contains(e.Id)).ToListAsync(ct).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Represents the read bulk operation by a collection of
    /// unique identifiers of <see cref="long"/> type.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public class NHReadByIdRange<TEntity> : NHReadByIdRange<TEntity, long>, IReadByIdRange<TEntity>
        where TEntity : class, IEntity<long>
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="query"></param>
        public NHReadByIdRange(IQueryable<TEntity> query) : base(query)
        {

        }
    }
}
