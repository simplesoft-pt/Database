using Microsoft.Extensions.DependencyInjection;
using System;

namespace SimpleSoft.Database
{
    /// <summary>
    /// The implementation compatible with NHibernate for the Unit of Work Factory.
    /// </summary>
    public class NHUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IServiceScopeFactory _scopeFactory;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="scopeFactory">The service scope factory to be used</param>
        public NHUnitOfWorkFactory(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        /// <inheritdoc />
        public IUnitOfWorkScoped Create() => new EFCoreUnitOfWorkScoped(_scopeFactory.CreateScope());

        private class EFCoreUnitOfWorkScoped : NHUnitOfWork, IUnitOfWorkScoped
        {
            private IServiceScope _scope;

            public EFCoreUnitOfWorkScoped(IServiceScope scope) : base(scope.ServiceProvider)
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

            private void Dispose(bool disposing)
            {
                if (disposing)
                    _scope?.Dispose();

                _scope = null;
            }

#if NETSTANDARD2_1

            /// <inheritdoc />
            public ValueTask DisposeAsync()
            {
                _scope?.Dispose();

                Dispose(false);
                GC.SuppressFinalize(this);

                return default;
            }

#endif
            #endregion
        }
    }
}
