using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the update operation
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    class NHUpdate<TEntity> : IUpdate<TEntity>
        where TEntity : class, IEntity
    {
        private readonly NHSessionContainer _container;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="container"></param>
        public NHUpdate(
            NHSessionContainer container
        )
        {
            _container = container;
        }

        /// <inheritdoc />
        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct)
        {
            return await _container.ExecuteAsync(async (ctx, single, c) =>
            {
                await ctx.UpdateAsync(single, ct).ConfigureAwait(false);
                return entity;
            }, entity, ct).ConfigureAwait(false);
        }
    }
}