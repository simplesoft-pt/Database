using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the delete operation
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public class NHDelete<TEntity> : IDelete<TEntity>
        where TEntity : class, IEntity
    {
        private readonly NHSessionContainer _container;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="container"></param>
        public NHDelete(
            NHSessionContainer container
        )
        {
            _container = container;
        }

        /// <inheritdoc />
        public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct)
        {
            return await _container.ExecuteAsync(async (ctx, single, c) =>
            {
                await ctx.DeleteAsync(single, ct);
                return single;
            }, entity, ct);
        }
    }
}
