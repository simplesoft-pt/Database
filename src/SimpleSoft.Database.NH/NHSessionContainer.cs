using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NHibernate;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Enables the access to the underline <see cref="ISession"/> instance.
    /// </summary>
    public class NHSessionContainer
    {
        private readonly ISession _session;
        private readonly NHSessionContainerOptions _options;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="session"></param>
        /// <param name="options"></param>
        public NHSessionContainer(
            ISession session,
            NHSessionContainerOptions options
        )
        {
            _session = session ?? throw new ArgumentNullException(nameof(session));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <summary>
        /// Returns an entity <see cref="IQueryable{T}"/> to apply LINQ expressions.
        /// </summary>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <returns></returns>
        public IQueryable<TEntity> Query<TEntity>() where TEntity : class => _session.Query<TEntity>();

        /// <summary>
        /// Enables the aggregation of database entities directly from the <see cref="ISession"/>.
        /// </summary>
        /// <typeparam name="TParam">The parameter type</typeparam>
        /// <typeparam name="TResult">The result type</typeparam>
        /// <param name="aggregator"></param>
        /// <param name="param"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<TResult> AggregateAsync<TParam, TResult>(
            Func<ISession, TParam, CancellationToken, Task<TResult>> aggregator,
            TParam param,
            CancellationToken ct
        )
        {
            if (aggregator == null) throw new ArgumentNullException(nameof(aggregator));

            return await aggregator(_session, param, ct);
        }

        /// <summary>
        /// Enables the mutation of database entities via a <see cref="ISession"/>.
        /// </summary>
        /// <remarks>
        /// This will call <see cref="FlushAsync(CancellationToken)"/> after executing
        /// the executor function if <see cref="NHSessionContainerOptions.AutoFlush"/> is
        /// set to 'true'.
        /// </remarks>
        /// <typeparam name="TParam">The parameter type</typeparam>
        /// <typeparam name="TResult">The result type</typeparam>
        /// <param name="executor"></param>
        /// <param name="param"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<TResult> ExecuteAsync<TParam, TResult>(
            Func<ISession, TParam, CancellationToken, Task<TResult>> executor,
            TParam param,
            CancellationToken ct
        )
        {
            if (executor == null) throw new ArgumentNullException(nameof(executor));

            var result = await executor(_session, param, ct);

            if (_options.AutoFlush)
                await FlushAsync(ct);

            return result;
        }

        /// <summary>
        /// Begins a transaction via <see cref="ISession"/>.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<NHibernate.ITransaction> BeginTransactionAsync(CancellationToken ct)
        {
            if (ct.IsCancellationRequested)
                return Task.FromCanceled<NHibernate.ITransaction>(ct);

            return Task.FromResult(
                _session.BeginTransaction(IsolationLevel.ReadCommitted)
            );
        }

        /// <summary>
        /// Flushes all <see cref="ISession"/> changes into the database.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task FlushAsync(CancellationToken ct)
        {
            await _session.FlushAsync(ct);
        }
    }
}
