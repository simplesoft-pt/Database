using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the delete operation in bulk
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface IDeleteRange<TEntity> 
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Deletes a range of entities
        /// </summary>
        /// <param name="entities">The entity collection</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The entity collection after changes</returns>
        Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken ct);
    }
}