using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the update operation
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface IUpdate<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The entity after changes</returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct);
    }
}
