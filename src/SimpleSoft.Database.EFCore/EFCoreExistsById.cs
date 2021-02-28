using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the exists operation by an unique identifier
    /// </summary>
    /// <typeparam name="TContext">The context type</typeparam>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The unique identifier type</typeparam>
    public class EFCoreExistsById<TContext, TEntity, TId> : IExistsById<TEntity, TId>
        where TContext : DbContext
        where TEntity : class, IEntity<TId>
        where TId : IEquatable<TId>
    {
        private readonly IQueryable<TEntity> _query;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="context"></param>
        public EFCoreExistsById(
            TContext context
        )
        {
            _query = context.Set<TEntity>().AsNoTracking();
        }

        /// <inheritdoc />
        public Task<bool> ExistsAsync(TId id, CancellationToken ct) => 
            _query.AnyAsync(e => e.Id.Equals(id), ct);
    }

    /// <summary>
    /// Represents the exists operation by an unique identifier
    /// of <see cref="long"/> type.
    /// </summary>
    /// <typeparam name="TContext">The context type</typeparam>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public class EFCoreExistsById<TContext, TEntity> : EFCoreExistsById<TContext, TEntity, long>, IExistsById<TEntity>
        where TContext : DbContext
        where TEntity : class, IEntity<long>
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="context"></param>
        public EFCoreExistsById(TContext context) : base(context)
        {

        }
    }
}
