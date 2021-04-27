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
            var transaction = _provider.GetRequiredService<EFCoreTransaction>();
            await transaction.BeginAsync(ct);
            return transaction;
        }

        /// <inheritdoc />
        public async Task<TEntity> CreateAsync<TEntity>(TEntity entity, CancellationToken ct) 
            where TEntity : class, IEntity
        {
            var service = _provider.GetRequiredService<EFCoreCreate<TEntity>>();
            return await service.CreateAsync(entity, ct);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> CreateAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken ct)
            where TEntity : class, IEntity
        {
            var service = _provider.GetRequiredService<EFCoreCreateRange<TEntity>>();

            return await service.CreateAsync(entities, ct);
        }

        /// <inheritdoc />
        public async Task<TEntity> DeleteAsync<TEntity>(TEntity entity, CancellationToken ct)
            where TEntity : class, IEntity
        {
            var service = _provider.GetRequiredService<EFCoreDelete<TEntity>>();
            return await service.DeleteAsync(entity, ct);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> DeleteAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken ct)
            where TEntity : class, IEntity
        {
            var service = _provider.GetRequiredService<EFCoreDeleteRange<TEntity>>();
            return await service.DeleteAsync(entities, ct);
        }

        /// <inheritdoc />
        public async Task<bool> ExistsByExternalIdAsync<TEntity, TId>(TId externalId, CancellationToken ct)
            where TEntity : class, IEntity, IHaveExternalId<TId>
            where TId : IEquatable<TId>
        {
            var service = _provider.GetRequiredService<EFCoreExistsByExternalId<TEntity, TId>>();
            return await service.ExistsAsync(externalId, ct);
        }

        /// <inheritdoc />
        public async Task<bool> ExistsByExternalIdAsync<TEntity>(Guid externalId, CancellationToken ct)
            where TEntity : class, IEntity, IHaveExternalId
        {
            var service = _provider.GetRequiredService<EFCoreExistsByExternalId<TEntity>>();
            return await service.ExistsAsync(externalId, ct);
        }

        /// <inheritdoc />
        public async Task<bool> ExistsByIdAsync<TEntity, TId>(TId id, CancellationToken ct)
            where TEntity : class, IEntity<TId>
            where TId : IEquatable<TId>
        {
            var service = _provider.GetRequiredService<EFCoreExistsById<TEntity, TId>>();
            return await service.ExistsAsync(id, ct);
        }

        /// <inheritdoc />
        public async Task<bool> ExistsByIdAsync<TEntity>(long id, CancellationToken ct)
            where TEntity : class, IEntity<long>
        {
            var service = _provider.GetRequiredService<EFCoreExistsById<TEntity>>();
            return await service.ExistsAsync(id, ct);
        }

        /// <inheritdoc />
        public IQueryable<TEntity> Query<TEntity>()
            where TEntity : class, IEntity
        {
            var service = _provider.GetRequiredService<EFCoreQueryable<TEntity>>();
            return service.AsQueryable();
        }

        /// <inheritdoc />
        public async Task<TEntity> ReadByExternalIdAsync<TEntity, TId>(TId externalId, CancellationToken ct)
            where TEntity : class, IEntity, IHaveExternalId<TId>
            where TId : IEquatable<TId>
        {
            var service = _provider.GetRequiredService<EFCoreReadByExternalId<TEntity, TId>>();
            return await service.ReadAsync(externalId, ct);
        }

        /// <inheritdoc />
        public async Task<TEntity> ReadByExternalIdAsync<TEntity>(Guid externalId, CancellationToken ct)
            where TEntity : class, IEntity, IHaveExternalId
        {
            var service = _provider.GetRequiredService<EFCoreReadByExternalId<TEntity>>();
            return await service.ReadAsync(externalId, ct);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> ReadByExternalIdAsync<TEntity, TId>(IEnumerable<TId> externalIds, CancellationToken ct)
            where TEntity : class, IEntity, IHaveExternalId<TId>
            where TId : IEquatable<TId>
        {
            var service = _provider.GetRequiredService<EFCoreReadByExternalIdRange<TEntity, TId>>();
            return await service.ReadAsync(externalIds, ct);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> ReadByExternalIdAsync<TEntity>(IEnumerable<Guid> externalIds, CancellationToken ct)
            where TEntity : class, IEntity, IHaveExternalId
        {
            var service = _provider.GetRequiredService<EFCoreReadByExternalIdRange<TEntity>>();
            return await service.ReadAsync(externalIds, ct);
        }

        /// <inheritdoc />
        public async Task<TEntity> ReadByIdAsync<TEntity, TId>(TId id, CancellationToken ct)
            where TEntity : class, IEntity<TId>
            where TId : IEquatable<TId>
        {
            var service = _provider.GetRequiredService<EFCoreReadById<TEntity, TId>>();
            return await service.ReadAsync(id, ct);
        }

        /// <inheritdoc />
        public async Task<TEntity> ReadByIdAsync<TEntity>(long id, CancellationToken ct)
            where TEntity : class, IEntity<long>
        {
            var service = _provider.GetRequiredService<EFCoreReadById<TEntity>>();
            return await service.ReadAsync(id, ct);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> ReadByIdAsync<TEntity, TId>(IEnumerable<TId> ids, CancellationToken ct)
            where TEntity : class, IEntity<TId>
            where TId : IEquatable<TId>
        {
            var service = _provider.GetRequiredService<EFCoreReadByIdRange<TEntity, TId>>();
            return await service.ReadAsync(ids, ct);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> ReadByIdAsync<TEntity>(IEnumerable<long> ids, CancellationToken ct)
            where TEntity : class, IEntity<long>
        {
            var service = _provider.GetRequiredService<EFCoreReadByIdRange<TEntity>>();
            return await service.ReadAsync(ids, ct);
        }

        /// <inheritdoc />
        public async Task<TEntity> UpdateAsync<TEntity>(TEntity entity, CancellationToken ct)
            where TEntity : class, IEntity
        {
            var service = _provider.GetRequiredService<EFCoreUpdate<TEntity>>();
            return await service.UpdateAsync(entity, ct);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> UpdateAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken ct)
            where TEntity : class, IEntity
        {
            var service = _provider.GetRequiredService<EFCoreUpdateRange<TEntity>>();
            return await service.UpdateAsync(entities, ct);
        }

        #region IDisposable

        /// <inheritdoc />
        ~EFCoreUnitOfWork() => Dispose(false);

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
        }

#if NETSTANDARD2_1

        /// <inheritdoc />
        public async ValueTask DisposeAsync()
        {
            await this.DisposeAsyncCore().ConfigureAwait(false);

            Dispose(disposing: false);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources asynchronously.
        /// </summary>
        /// <returns>A completed task of this operation</returns>
        protected virtual ValueTask DisposeAsyncCore()
        {
            return default;
        }
#endif

        #endregion

    }
}
