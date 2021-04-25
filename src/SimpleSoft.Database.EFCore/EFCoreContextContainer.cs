using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Enables the access to the underline <see cref="DbContext"/> instance.
    /// </summary>
    public class EFCoreContextContainer
    {
        private readonly DbContext _context;
        private readonly EFCoreContextContainerOptions _options;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="context"></param>
        /// <param name="options"></param>
        public EFCoreContextContainer(
            DbContext context,
            IOptions<EFCoreContextContainerOptions> options
        )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _options = (options ?? throw new ArgumentNullException(nameof(options))).Value;
        }

        /// <summary>
        /// Returns an entity <see cref="IQueryable{T}"/> to apply LINQ expressions.
        /// </summary>
        /// <remarks>
        /// This will disable Entity Framework change tracking if
        /// <see cref="EFCoreContextContainerOptions.NoTracking"/> is set to 'true'.
        /// </remarks>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <returns>The entity queryable</returns>
        public IQueryable<TEntity> Query<TEntity>() where TEntity : class
        {
            var trackingBehavior = _context.ChangeTracker.QueryTrackingBehavior;
            var set = _context.Set<TEntity>();

            return _options.NoTracking
                ? trackingBehavior == QueryTrackingBehavior.NoTracking
                    ? set
                    : set.AsNoTracking()
                : trackingBehavior == QueryTrackingBehavior.TrackAll
                    ? set
                    : set.AsTracking();
        }

        /// <summary>
        /// Enables the aggregation of database entities directly from the <see cref="DbContext"/>.
        /// </summary>
        /// <typeparam name="TParam">The parameter type</typeparam>
        /// <typeparam name="TResult">The result type</typeparam>
        /// <param name="aggregator"></param>
        /// <param name="param"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<TResult> AggregateAsync<TParam, TResult>(
            Func<DbContext, TParam, CancellationToken, Task<TResult>> aggregator,
            TParam param,
            CancellationToken ct
        )
        {
            if (aggregator == null) throw new ArgumentNullException(nameof(aggregator));

            return await aggregator(_context, param, ct);
        }

        /// <summary>
        /// Enables the mutation of database entities via a <see cref="DbContext"/>.
        /// </summary>
        /// <remarks>
        /// This will call <see cref="EFCoreContextContainer.SaveChangesAsync(CancellationToken)"/> after
        /// executing the executor function if <see cref="EFCoreContextContainerOptions.AutoSaveChanges"/> is
        /// set to 'true'.
        /// </remarks>
        /// <typeparam name="TParam">The parameter type</typeparam>
        /// <typeparam name="TResult">The result type</typeparam>
        /// <param name="executor"></param>
        /// <param name="param"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<TResult> ExecuteAsync<TParam, TResult>(
            Func<DbContext, TParam, CancellationToken, Task<TResult>> executor,
            TParam param,
            CancellationToken ct
        )
        {
            if (executor == null) throw new ArgumentNullException(nameof(executor));

            var result = await executor(_context, param, ct);

            if (_options.AutoSaveChanges)
                await SaveChangesAsync(ct);

            return result;
        }

        /// <summary>
        /// Begins a transaction asynchronously via <see cref="DbContext"/>.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the newly created transaction. <see cref="IDbContextTransaction"/></returns>
        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct)
        {
            return _context.Database.BeginTransactionAsync(ct);
        }

        /// <summary>
        /// Asynchronously persists all changes made to this context <see cref="DbContext.SaveChangesAsync(CancellationToken)"/>.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns>A completed task of this operation</returns>
        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _context.SaveChangesAsync(ct);
        }
    }
}
