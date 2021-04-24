using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <inheritdoc />
    public class EFCoreTransaction : ITransaction
    {
        private readonly EFCoreContextContainer _container;
        private IDbContextTransaction _transaction;
        private bool _disposed;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="container"></param>
        public EFCoreTransaction(EFCoreContextContainer container)
        {
            _container = container;
        }

        #region IDisposable

        /// <inheritdoc />
        ~EFCoreTransaction() => Dispose(false);

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
            if (_disposed)
                return;

            if (disposing)
            {

#if NETSTANDARD2_1
                DisposeAsync()
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();
#else
                _transaction?.Dispose();
#endif
            }

            _transaction = null;
            _disposed = true;
        }

#if NETSTANDARD2_1 
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources asynchronously.
        /// </summary>
        /// <returns>A completed task of this operation</returns>
        public virtual async ValueTask DisposeAsync()
        {
            if (_disposed)
                return;

            if (_transaction != null)
                await _transaction.DisposeAsync();

            _transaction = null;
            _disposed = true;
        }
#endif

        #endregion

        /// <inheritdoc />
        public async Task BeginAsync(CancellationToken ct)
        {
            if (_transaction != null)
                throw new InvalidOperationException("Transaction already open.");

            _transaction = await _container.BeginTransactionAsync(ct);
        }

        /// <inheritdoc />
        public async Task CommitAsync(CancellationToken ct)
        {
            if (_transaction == null) throw new InvalidOperationException("Transaction must be open.");

            await _container.SaveChangesAsync(ct);

#if NETSTANDARD2_1
            await _transaction.CommitAsync(ct);
#else
            _transaction.Commit();
#endif
        }

        /// <inheritdoc />
        public Task RollbackAsync(CancellationToken ct)
        {
            if (_transaction == null) throw new InvalidOperationException("Transaction must be open.");

#if NETSTANDARD2_1
            return _transaction.RollbackAsync(ct);
#else
            _transaction.Rollback();
            return Task.CompletedTask;
#endif
        }
    }
}
