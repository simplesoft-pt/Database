using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the create operation
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public class EFCoreCreate<TEntity> : ICreate<TEntity>
        where TEntity : class, IEntity
    {
        private readonly EFCoreContextContainer _container;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="container"></param>
        public EFCoreCreate(
            EFCoreContextContainer container
        )
        {
            _container = container;
        }

        /// <inheritdoc />
        public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken ct)
        {
            return await _container.ExecuteAsync(async (ctx, single, c) =>
            {
                var entry = await ctx.Set<TEntity>().AddAsync(single, c).ConfigureAwait(false);
                return entry.Entity;
            }, entity, ct).ConfigureAwait(false);
        }
    }
}
