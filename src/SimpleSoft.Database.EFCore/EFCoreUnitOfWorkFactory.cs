using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <summary>
    /// The implementation compatible with Entity Framework Core for the Unit of Work Factory.
    /// </summary>
    public class EFCoreUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IServiceScopeFactory _scopeFactory;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="scopeFactory">The service scope factory to be used</param>
        public EFCoreUnitOfWorkFactory(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        /// <inheritdoc />
        public IUnitOfWorkScoped Create() =>
            new EFCoreUnitOfWorkScoped(_scopeFactory.CreateScope());

        private class EFCoreUnitOfWorkScoped : EFCoreUnitOfWork, IUnitOfWorkScoped
        {
            private IServiceScope _scope;
            public EFCoreUnitOfWorkScoped(IServiceScope scope)
                : base(scope.ServiceProvider)
            {
                _scope = scope;
            }

            #region IDisposable

            ~EFCoreUnitOfWorkScoped() => Dispose(false);

            /// <inheritdoc />
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected void Dispose(bool disposing)
            {
                if (disposing)
                    _scope?.Dispose();

                _scope = null;
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
                _scope?.Dispose();

                return default;
            }
#endif
            #endregion
        }
    }
}
