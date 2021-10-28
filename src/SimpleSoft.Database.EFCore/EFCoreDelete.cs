using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the delete operation
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public class EFCoreDelete<TEntity> : IDelete<TEntity>
        where TEntity : class, IEntity
    {
        private readonly EFCoreContextContainer _container;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="container"></param>
        public EFCoreDelete(
            EFCoreContextContainer container
        )
        {
            _container = container;
        }

        /// <inheritdoc />
        public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct)
        {
            return await _container.ExecuteAsync((ctx, single, c) =>
            {
                var entry = ctx.Set<TEntity>().Remove(single);
                return Task.FromResult(entry.Entity);
            }, entity, ct).ConfigureAwait(false);
        }
    }
}
