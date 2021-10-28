using NHibernate.Linq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the exists operation by an unique identifier
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The unique identifier type</typeparam>
    public class NHExistsById<TEntity, TId> : IExistsById<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId : IEquatable<TId>
    {
        private readonly IQueryable<TEntity> _query;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="query"></param>
        public NHExistsById(IQueryable<TEntity> query)
        {
            _query = query;
        }

        /// <inheritdoc />
        public async Task<bool> ExistsAsync(TId id, CancellationToken ct) =>
            await _query.AnyAsync(e => e.Id.Equals(id), ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Represents the exists operation by an unique identifier
    /// of <see cref="long"/> type.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public class NHExistsById<TEntity> : NHExistsById<TEntity, long>, IExistsById<TEntity>
        where TEntity : class, IEntity<long>
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="query"></param>
        public NHExistsById(IQueryable<TEntity> query) : base(query)
        {

        }
    }
}
