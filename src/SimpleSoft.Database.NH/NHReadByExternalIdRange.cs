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
    /// external unique identifiers
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The unique identifier type</typeparam>
    public class NHReadByExternalIdRange<TEntity, TId> : IReadByExternalIdRange<TEntity, TId>
        where TEntity : class, IEntity, IHaveExternalId<TId>
        where TId : IEquatable<TId>
    {
        private readonly NHSessionContainer _container;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="container"></param>
        public NHReadByExternalIdRange(
            NHSessionContainer container
        )
        {
            _container = container;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> ReadAsync(IEnumerable<TId> externalIds, CancellationToken ct)
        {
            if (externalIds == null) throw new ArgumentNullException(nameof(externalIds));

            return await _container.Query<TEntity>().Where(e => externalIds.Contains(e.ExternalId)).ToListAsync(ct);
        }
    }

    /// <summary>
    /// Represents the exists operation by an external unique identifier
    /// of <see cref="Guid"/> type.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public class NHReadByExternalIdRange<TEntity> : NHReadByExternalIdRange<TEntity, Guid>, IReadByExternalIdRange<TEntity>
        where TEntity : class, IEntity, IHaveExternalId
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="container"></param>
        public NHReadByExternalIdRange(NHSessionContainer container) : base(container)
        {

        }
    }
}
