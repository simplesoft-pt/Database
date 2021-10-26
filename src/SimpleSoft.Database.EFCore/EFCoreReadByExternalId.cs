using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the read operation by an external unique identifier
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The unique identifier type</typeparam>
    public class EFCoreReadByExternalId<TEntity, TId> : IReadByExternalId<TEntity, TId>
        where TEntity : class, IEntity, IHaveExternalId<TId>
        where TId : IEquatable<TId>
    {
        private readonly IQueryable<TEntity> _query;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="query"></param>
        public EFCoreReadByExternalId(IQueryable<TEntity> query)
        {
            _query = query;
        }

        /// <inheritdoc />
        public async Task<TEntity> ReadAsync(TId externalId, CancellationToken ct) =>
            await _query.SingleOrDefaultAsync(e => e.ExternalId.Equals(externalId), ct);
    }

    /// <summary>
    /// Represents the read operation by an external unique identifier
    /// of <see cref="Guid"/> type.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public class EFCoreReadByExternalId<TEntity> : EFCoreReadByExternalId<TEntity, Guid>, IReadByExternalId<TEntity>
        where TEntity : class, IEntity, IHaveExternalId
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="query"></param>
        public EFCoreReadByExternalId(IQueryable<TEntity> query) : base(query)
        {

        }
    }
}
