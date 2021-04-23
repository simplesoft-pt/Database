using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Extensions for <see cref="EFCoreContextContainer"/> instances.
    /// </summary>
    public static class EFCoreContextContainerExtensions
    {
        /// <summary>
        /// Enables the aggregation of database entities directly from the <see cref="DbContext"/>.
        /// </summary>
        /// <typeparam name="TResult">The result type</typeparam>
        /// <param name="container"></param>
        /// <param name="aggregator"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException" />
        public static Task<TResult> AggregateAsync<TResult>(
            this EFCoreContextContainer container,
            Func<DbContext, CancellationToken, Task<TResult>> aggregator,
            CancellationToken ct
        )
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (aggregator == null) throw new ArgumentNullException(nameof(aggregator));

            return container.AggregateAsync((ctx, agg, c) => agg(ctx, c), aggregator, ct);
        }

        /// <summary>
        /// Enables the mutation of database entities via a <see cref="DbContext"/>.
        /// </summary>
        /// <remarks>
        /// This will call <see cref="DbContext.SaveChangesAsync(CancellationToken)"/> after
        /// executing the executor function if <see cref="EFCoreContextContainerOptions.AutoSaveChanges"/> is
        /// set to 'true'.
        /// </remarks>
        /// <typeparam name="TResult">The result type</typeparam>
        /// <param name="container"></param>
        /// <param name="executor"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException" />
        public static Task<TResult> ExecuteAsync<TResult>(
            this EFCoreContextContainer container,
            Func<DbContext, CancellationToken, Task<TResult>> executor,
            CancellationToken ct
        )
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (executor == null) throw new ArgumentNullException(nameof(executor));

            return container.ExecuteAsync((ctx, exec, c) => exec(ctx, c), executor, ct);
        }

        /// <summary>
        /// Enables the mutation of database entities via a <see cref="DbContext"/>.
        /// </summary>
        /// <remarks>
        /// This will call <see cref="DbContext.SaveChangesAsync(CancellationToken)"/> after
        /// executing the executor function if <see cref="EFCoreContextContainerOptions.AutoSaveChanges"/> is
        /// set to 'true'.
        /// </remarks>
        /// <typeparam name="TParam">The parameter type</typeparam>
        /// <param name="container"></param>
        /// <param name="executor"></param>
        /// <param name="param"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException" />
        public static Task ExecuteAsync<TParam>(
            this EFCoreContextContainer container,
            Func<DbContext, TParam, CancellationToken, Task> executor,
            TParam param,
            CancellationToken ct
        )
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (executor == null) throw new ArgumentNullException(nameof(executor));


#if NETSTANDARD2_1
            var innerParam = (param, executor);
#else
            var innerParam = new
            {
                param,
                executor
            };
#endif

            return container.ExecuteAsync(async (ctx, p, c) =>
            {
                await p.executor(ctx, p.param, c);
                return 0;
            }, innerParam, ct);
        }

        /// <summary>
        /// Enables the mutation of database entities via a <see cref="DbContext"/>.
        /// </summary>
        /// <remarks>
        /// This will call <see cref="DbContext.SaveChangesAsync(CancellationToken)"/> after
        /// executing the executor function if <see cref="EFCoreContextContainerOptions.AutoSaveChanges"/> is
        /// set to 'true'.
        /// </remarks>
        /// <param name="container"></param>
        /// <param name="executor"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException" />
        public static Task ExecuteAsync(
            this EFCoreContextContainer container,
            Func<DbContext, CancellationToken, Task> executor,
            CancellationToken ct
        )
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (executor == null) throw new ArgumentNullException(nameof(executor));

            return container.ExecuteAsync(async (ctx, exec, c) =>
            {
                await exec(ctx, c);
                return 0;
            }, executor, ct);
        }
    }
}