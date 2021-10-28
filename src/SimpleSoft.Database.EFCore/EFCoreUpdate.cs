using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the update operation
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public class EFCoreUpdate<TEntity> : IUpdate<TEntity>
        where TEntity : class, IEntity
    {
        private readonly EFCoreContextContainer _container;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="container"></param>
        public EFCoreUpdate(
            EFCoreContextContainer container
        )
        {
            _container = container;
        }

        /// <inheritdoc />
        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct)
        {
            return await _container.ExecuteAsync((ctx, single, c) =>
            {
                var entry = ctx.Set<TEntity>().Update(single);
                return Task.FromResult(entry.Entity);
            }, entity, ct).ConfigureAwait(false);
        }
    }
}
