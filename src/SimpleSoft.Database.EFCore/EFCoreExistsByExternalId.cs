using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the exists operation by an external unique identifier
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The unique identifier type</typeparam>
    public class EFCoreExistsByExternalId<TEntity, TId> : IExistsByExternalId<TEntity, TId>
        where TEntity : class, IEntity, IHaveExternalId<TId>
        where TId : IEquatable<TId>
    {
        private readonly IQueryable<TEntity> _query;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="query"></param>
        public EFCoreExistsByExternalId(IQueryable<TEntity> query)
        {
            _query = query;
        }

        /// <inheritdoc />
        public async Task<bool> ExistsAsync(TId externalId, CancellationToken ct) =>
            await _query.AnyAsync(e => e.ExternalId.Equals(externalId), ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Represents the exists operation by an external unique identifier
    /// of <see cref="Guid"/> type.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public class EFCoreExistsByExternalId<TEntity> : EFCoreExistsByExternalId<TEntity, Guid>, IExistsByExternalId<TEntity>
        where TEntity : class, IEntity, IHaveExternalId
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="query"></param>
        public EFCoreExistsByExternalId(IQueryable<TEntity> query) : base(query)
        {

        }
    }
}
