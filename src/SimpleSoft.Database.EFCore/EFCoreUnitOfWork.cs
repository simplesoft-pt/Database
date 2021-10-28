using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleSoft.Database
{
    /// <summary>
    /// An implementation compatible with Entity Framework Core for the Unit of Work pattern.
    /// </summary>
    public class EFCoreUnitOfWork : IUnitOfWork
    {
        private readonly IServiceProvider _provider;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="provider"></param>
        public EFCoreUnitOfWork(IServiceProvider provider)
        {
            _provider = provider;
        }

        /// <inheritdoc />
        public async Task<ITransaction> BeginTransactionAsync(CancellationToken ct)
        {
            var transaction = Service<ITransaction>();

            try
            {
                await transaction.BeginAsync(ct).ConfigureAwait(false);
            }
            catch
            {
#if NETSTANDARD2_1
                await transaction.DisposeAsync().ConfigureAwait(false);
#else
                transaction.Dispose();
#endif
                throw;
            }

            return transaction;
        }

        /// <inheritdoc />
        public async Task<TEntity> CreateAsync<TEntity>(TEntity entity, CancellationToken ct) 
            where TEntity : class, IEntity
        {
            return await Service<ICreate<TEntity>>().CreateAsync(entity, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> CreateAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken ct)
            where TEntity : class, IEntity
        {
            return await Service<ICreateRange<TEntity>>().CreateAsync(entities, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<TEntity> DeleteAsync<TEntity>(TEntity entity, CancellationToken ct)
            where TEntity : class, IEntity
        {
            return await Service<IDelete<TEntity>>().DeleteAsync(entity, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> DeleteAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken ct)
            where TEntity : class, IEntity
        {
            return await Service<IDeleteRange<TEntity>>().DeleteAsync(entities, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> ExistsByExternalIdAsync<TEntity, TId>(TId externalId, CancellationToken ct)
            where TEntity : class, IEntity, IHaveExternalId<TId>
            where TId : IEquatable<TId>
        {
            return await Service<IExistsByExternalId<TEntity, TId>>().ExistsAsync(externalId, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> ExistsByExternalIdAsync<TEntity>(Guid externalId, CancellationToken ct)
            where TEntity : class, IEntity, IHaveExternalId
        {
            return await Service<IExistsByExternalId<TEntity>>().ExistsAsync(externalId, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> ExistsByIdAsync<TEntity, TId>(TId id, CancellationToken ct)
            where TEntity : class, IEntity<TId>
            where TId : IEquatable<TId>
        {
            return await Service<IExistsById<TEntity, TId>>().ExistsAsync(id, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> ExistsByIdAsync<TEntity>(long id, CancellationToken ct)
            where TEntity : class, IEntity<long>
        {
            return await Service<IExistsById<TEntity>>().ExistsAsync(id, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public IQueryable<TEntity> Query<TEntity>()
            where TEntity : class, IEntity
        {
            return Service<IQueryable<TEntity>>();
        }

        /// <inheritdoc />
        public async Task<TEntity> ReadByExternalIdAsync<TEntity, TId>(TId externalId, CancellationToken ct)
            where TEntity : class, IEntity, IHaveExternalId<TId>
            where TId : IEquatable<TId>
        {
            return await Service<IReadByExternalId<TEntity, TId>>().ReadAsync(externalId, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<TEntity> ReadByExternalIdAsync<TEntity>(Guid externalId, CancellationToken ct)
            where TEntity : class, IEntity, IHaveExternalId
        {
            return await Service<IReadByExternalId<TEntity>>().ReadAsync(externalId, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> ReadByExternalIdAsync<TEntity, TId>(IEnumerable<TId> externalIds, CancellationToken ct)
            where TEntity : class, IEntity, IHaveExternalId<TId>
            where TId : IEquatable<TId>
        {
            return await Service<IReadByExternalIdRange<TEntity, TId>>().ReadAsync(externalIds, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> ReadByExternalIdAsync<TEntity>(IEnumerable<Guid> externalIds, CancellationToken ct)
            where TEntity : class, IEntity, IHaveExternalId
        {
            return await Service<IReadByExternalIdRange<TEntity>>().ReadAsync(externalIds, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<TEntity> ReadByIdAsync<TEntity, TId>(TId id, CancellationToken ct)
            where TEntity : class, IEntity<TId>
            where TId : IEquatable<TId>
        {
            return await Service<IReadById<TEntity, TId>>().ReadAsync(id, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<TEntity> ReadByIdAsync<TEntity>(long id, CancellationToken ct)
            where TEntity : class, IEntity<long>
        {
            return await Service<IReadById<TEntity>>().ReadAsync(id, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> ReadByIdAsync<TEntity, TId>(IEnumerable<TId> ids, CancellationToken ct)
            where TEntity : class, IEntity<TId>
            where TId : IEquatable<TId>
        {
            return await Service<IReadByIdRange<TEntity, TId>>().ReadAsync(ids, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> ReadByIdAsync<TEntity>(IEnumerable<long> ids, CancellationToken ct)
            where TEntity : class, IEntity<long>
        {
            return await Service<IReadByIdRange<TEntity>>().ReadAsync(ids, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<TEntity> UpdateAsync<TEntity>(TEntity entity, CancellationToken ct)
            where TEntity : class, IEntity
        {
            return await Service<IUpdate<TEntity>>().UpdateAsync(entity, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> UpdateAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken ct)
            where TEntity : class, IEntity
        {
            return await Service<IUpdateRange<TEntity>>().UpdateAsync(entities, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get service of type <typeparamref name="T"/> from the <see cref="IServiceProvider"/>.
        /// </summary>
        /// <typeparam name="T">The type of service object to get.</typeparam>
        /// <returns>A service object of type <typeparamref name="T"/>.</returns>
        protected T Service<T>() => _provider.GetRequiredService<T>();
    }
}
