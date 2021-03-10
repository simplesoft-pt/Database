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
    /// <typeparam name="TContext">The context type</typeparam>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The unique identifier type</typeparam>
    public class EFCoreExistsByExternalId<TContext, TEntity, TId> : IExistsByExternalId<TEntity, TId>
        where TContext : DbContext
        where TEntity : class, IEntity, IHaveExternalId<TId>
        where TId : IEquatable<TId>
    {
        private readonly IQueryable<TEntity> _query;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="context"></param>
        public EFCoreExistsByExternalId(
            TContext context
        )
        {
            _query = context.Set<TEntity>().AsNoTracking();
        }

        /// <inheritdoc />
        public async Task<bool> ExistsAsync(TId externalId, CancellationToken ct) => 
            await _query.AnyAsync(e => e.ExternalId.Equals(externalId), ct);
    }

    /// <summary>
    /// Represents the exists operation by an external unique identifier
    /// of <see cref="Guid"/> type.
    /// </summary>
    /// <typeparam name="TContext">The context type</typeparam>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public class EFCoreExistsByExternalId<TContext, TEntity> : EFCoreExistsByExternalId<TContext, TEntity, Guid>, IExistsByExternalId<TEntity>
        where TContext : DbContext
        where TEntity : class, IEntity, IHaveExternalId
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="context"></param>
        public EFCoreExistsByExternalId(TContext context) : base(context)
        {

        }
    }
}
