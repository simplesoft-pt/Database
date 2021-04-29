using System;

namespace SimpleSoft.Database
{
    /// <summary>
	/// Interface that represents the scoped unit of work.
	/// </summary>
    public interface IUnitOfWorkScoped : IUnitOfWork, IDisposable
#if NETSTANDARD2_1 
        , IAsyncDisposable
#endif
    {
    }
}