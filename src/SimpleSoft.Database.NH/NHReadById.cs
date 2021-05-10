using NHibernate.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the read operation by a unique identifier
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The unique identifier type</typeparam>
    public class NHReadById<TEntity, TId> : IReadById<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId : IEquatable<TId>
    {
        private readonly NHSessionContainer _container;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="container"></param>
        public NHReadById(
            NHSessionContainer container
        )
        {
            _container = container;
        }

        /// <inheritdoc />
        public async Task<TEntity> ReadAsync(TId id, CancellationToken ct) =>
            await _container.Query<TEntity>().SingleOrDefaultAsync(e => e.Id.Equals(id), ct);
    }

    /// <summary>
    /// Represents the read operation by a long unique identifier
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public class NHReadById<TEntity> : NHReadById<TEntity, long>, IReadById<TEntity>
        where TEntity : class, IEntity<long>
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="container"></param>
        public NHReadById(NHSessionContainer container) : base(container)
        {

        }
    }
}
