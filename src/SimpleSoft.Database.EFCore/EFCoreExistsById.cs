using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the exists operation by an unique identifier
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The unique identifier type</typeparam>
    public class EFCoreExistsById<TEntity, TId> : IExistsById<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId : IEquatable<TId>
    {
        private readonly EFCoreContextContainer _container;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="container"></param>
        public EFCoreExistsById(
            EFCoreContextContainer container
        )
        {
            _container = container;
        }

        /// <inheritdoc />
        public async Task<bool> ExistsAsync(TId id, CancellationToken ct) => 
            await _container.Query<TEntity>().AnyAsync(e => e.Id.Equals(id), ct);
    }

    /// <summary>
    /// Represents the exists operation by an unique identifier
    /// of <see cref="long"/> type.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public class EFCoreExistsById<TEntity> : EFCoreExistsById<TEntity, long>, IExistsById<TEntity>
        where TEntity : class, IEntity<long>
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="container"></param>
        public EFCoreExistsById(EFCoreContextContainer container) : base(container)
        {

        }
    }
}
