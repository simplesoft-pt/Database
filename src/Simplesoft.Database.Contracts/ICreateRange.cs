using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the create operation in bulk
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ICreateRange<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// Creates a range of entities
        /// </summary>
        /// <param name="entities">The entity collection</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The entity collection after changes</returns>
        Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entities, CancellationToken ct);

        /// <summary>
        /// Creates a range of entities
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <param name="entities">The entity collection</param>
        /// <returns>The entity collection after changes</returns>
        Task<IEnumerable<TEntity>> CreateRangeAsync(CancellationToken ct, params TEntity[] entities);
    }
}