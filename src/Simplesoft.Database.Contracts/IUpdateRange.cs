using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the update operation in bulk
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IUpdateRange<TEntity> 
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Updates a range of entities
        /// </summary>
        /// <param name="entities">The entity collection</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The entity collection after changes</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities, CancellationToken ct);

        /// <summary>
        /// Updates a range of entities
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <param name="entities">The entity collection</param>
        /// <returns>The entity collection after changes</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(CancellationToken ct, params TEntity[] entities);
    }
}