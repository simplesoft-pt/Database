using System;
using System.Threading;
using System.Threading.Tasks;
using NHibernate;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Extensions for <see cref="NHSessionContainer"/> instances.
    /// </summary>
    public static class NHSessionContainerExtensions
    {
        /// <summary>
        /// Enables the aggregation of database entities directly from the <see cref="ISession"/>.
        /// </summary>
        /// <typeparam name="TResult">The result type</typeparam>
        /// <param name="container"></param>
        /// <param name="aggregator"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException" />
        public static Task<TResult> AggregateAsync<TResult>(
            this NHSessionContainer container,
            Func<ISession, CancellationToken, Task<TResult>> aggregator,
            CancellationToken ct
        )
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (aggregator == null) throw new ArgumentNullException(nameof(aggregator));

            return container.AggregateAsync((ctx, agg, c) => agg(ctx, c), aggregator, ct);
        }

        /// <summary>
        /// Enables the mutation of database entities via a <see cref="ISession"/>.
        /// </summary>
        /// <remarks>
        /// This will call <see cref="NHSessionContainer.FlushAsync(CancellationToken)"/> after executing
        /// the executor function if <see cref="NHSessionContainerOptions.AutoFlush"/> is
        /// set to 'true'.
        /// </remarks>
        /// <typeparam name="TResult">The result type</typeparam>
        /// <param name="container"></param>
        /// <param name="executor"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException" />
        public static Task<TResult> ExecuteAsync<TResult>(
            this NHSessionContainer container,
            Func<ISession, CancellationToken, Task<TResult>> executor,
            CancellationToken ct
        )
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (executor == null) throw new ArgumentNullException(nameof(executor));

            return container.ExecuteAsync((ctx, exec, c) => exec(ctx, c), executor, ct);
        }

        /// <summary>
        /// Enables the mutation of database entities via a <see cref="ISession"/>.
        /// </summary>
        /// <remarks>
        /// This will call <see cref="NHSessionContainer.FlushAsync(CancellationToken)"/> after executing
        /// the executor function if <see cref="NHSessionContainerOptions.AutoFlush"/> is
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
            this NHSessionContainer container,
            Func<ISession, TParam, CancellationToken, Task> executor,
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
        /// Enables the mutation of database entities via a <see cref="ISession"/>.
        /// </summary>
        /// <remarks>
        /// This will call <see cref="NHSessionContainer.FlushAsync(CancellationToken)"/> after executing
        /// the executor function if <see cref="NHSessionContainerOptions.AutoFlush"/> is
        /// set to 'true'.
        /// </remarks>
        /// <param name="container"></param>
        /// <param name="executor"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException" />
        public static Task ExecuteAsync(
            this NHSessionContainer container,
            Func<ISession, CancellationToken, Task> executor,
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