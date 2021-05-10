using NHibernate.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the exists operation by an external unique identifier
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The unique identifier type</typeparam>
    public class NHExistsByExternalId<TEntity, TId> : IExistsByExternalId<TEntity, TId>
        where TEntity : class, IEntity, IHaveExternalId<TId>
        where TId : IEquatable<TId>
    {
        private readonly NHSessionContainer _container;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="container"></param>
        public NHExistsByExternalId(
            NHSessionContainer container
        )
        {
            _container = container;
        }

        /// <inheritdoc />
        public async Task<bool> ExistsAsync(TId externalId, CancellationToken ct) =>
            await _container.Query<TEntity>().AnyAsync(e => e.ExternalId.Equals(externalId), ct);
    }

    /// <summary>
    /// Represents the exists operation by an external unique identifier
    /// of <see cref="Guid"/> type.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public class NHExistsByExternalId<TEntity> : NHExistsByExternalId<TEntity, Guid>, IExistsByExternalId<TEntity>
        where TEntity : class, IEntity, IHaveExternalId
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="container"></param>
        public NHExistsByExternalId(NHSessionContainer container) : base(container)
        {

        }
    }
}
