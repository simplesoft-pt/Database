using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the read operation by a unique identifier
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The unique identifier type</typeparam>
    public class EFCoreReadById<TEntity, TId> : IReadById<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId : IEquatable<TId>
    {
        private readonly IQueryable<TEntity> _query;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="query"></param>
        public EFCoreReadById(IQueryable<TEntity> query)
        {
            _query = query;
        }

        /// <inheritdoc />
        public async Task<TEntity> ReadAsync(TId id, CancellationToken ct) =>
            await _query.SingleOrDefaultAsync(e => e.Id.Equals(id), ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Represents the read operation by a long unique identifier
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public class EFCoreReadById<TEntity> : EFCoreReadById<TEntity, long>, IReadById<TEntity>
        where TEntity : class, IEntity<long>
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="query"></param>
        public EFCoreReadById(IQueryable<TEntity> query) : base(query)
        {

        }
    }
}
