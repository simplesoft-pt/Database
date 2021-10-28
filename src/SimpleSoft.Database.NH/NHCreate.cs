using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the create operation
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public class NHCreate<TEntity> : ICreate<TEntity>
        where TEntity : class, IEntity
    {
        private readonly NHSessionContainer _container;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="container"></param>
        public NHCreate(
            NHSessionContainer container
        )
        {
            _container = container;
        }

        /// <inheritdoc />
        public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken ct)
        {
            return await _container.ExecuteAsync(async (ctx, single, c) =>
            {
                await ctx.SaveAsync(entity, ct).ConfigureAwait(false);
                return entity;
            }, entity, ct).ConfigureAwait(false);
        }
    }
}
