using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Creates and manages the current transaction
    /// </summary>
    public interface ITransaction : IDisposable
#if NETSTANDARD2_1 
    , IAsyncDisposable 
#endif
    {
        /// <summary>
        /// Asynchronously begins a new transaction. />.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns>A completed task of this operation</returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task BeginAsync(CancellationToken ct);

        /// <summary>
        /// Commits all changes made to the database in the current transaction asynchronously. />.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns>A completed task of this operation</returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task CommitAsync(CancellationToken ct);

        /// <summary>
        /// Discards all changes made to the database in the current transaction asynchronously. />.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns>A completed task of this operation</returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task RollbackAsync(CancellationToken ct);
    }
}