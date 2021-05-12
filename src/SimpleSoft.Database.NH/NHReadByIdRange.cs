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
        private readonly NHSessionContainer _container;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="container"></param>
        public NHReadByIdRange(
            NHSessionContainer container
        )
        {
            _container = container;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> ReadAsync(IEnumerable<TId> ids, CancellationToken ct)
        {
            if (ids == null) throw new ArgumentNullException(nameof(ids));

            return await _container.Query<TEntity>().Where(e => ids.Contains(e.Id)).ToListAsync(ct);
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
        /// <param name="container"></param>
        public NHReadByIdRange(NHSessionContainer container) : base(container)
        {

        }
    }
}
