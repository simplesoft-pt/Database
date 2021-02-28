using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the create operation
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface ICreate<TEntity> 
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Creates an entity
        /// </summary>
        /// <param name="entity">The entity to create</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The entity after changes</returns>
        Task<TEntity> CreateAsync(TEntity entity, CancellationToken ct);
    }
}
