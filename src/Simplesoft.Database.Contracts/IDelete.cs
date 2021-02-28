using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the delete operation
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface IDelete<TEntity> 
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The entity after changes</returns>
        Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct);
    }
}
