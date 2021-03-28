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
    /// <typeparam name="TContext">The context type</typeparam>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The unique identifier type</typeparam>
    public class EFCoreReadByExternalId<TContext, TEntity, TId> : IReadByExternalId<TEntity, TId>
        where TContext : DbContext
        where TEntity : class, IEntity, IHaveExternalId<TId>
        where TId : IEquatable<TId>
    {
        private readonly IQueryable<TEntity> _query;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="context"></param>
        public EFCoreReadByExternalId(
            TContext context
        )
        {
            _query = context.Set<TEntity>().AsNoTracking();
        }

        /// <inheritdoc />
        public async Task<TEntity> ReadAsync(TId externalId, CancellationToken ct) =>
            await _query.SingleOrDefaultAsync(e => e.ExternalId.Equals(externalId), ct);
    }

    /// <summary>
    /// Represents the read operation by an external unique identifier
    /// of <see cref="Guid"/> type.
    /// </summary>
    /// <typeparam name="TContext">The context type</typeparam>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public class EFCoreReadByExternalId<TContext, TEntity> : EFCoreReadByExternalId<TContext, TEntity, Guid>, IReadByExternalId<TEntity>
        where TContext : DbContext
        where TEntity : class, IEntity, IHaveExternalId
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="context"></param>
        public EFCoreReadByExternalId(TContext context) : base(context)
        {

        }
    }
}
