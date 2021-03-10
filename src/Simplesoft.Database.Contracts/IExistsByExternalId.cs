using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the exists operation by an external unique identifier
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The unique identifier type</typeparam>
    public interface IExistsByExternalId<TEntity, in TId>
        where TEntity : class, IEntity, IHaveExternalId<TId>
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// Checks if an entity exists for a given unique identifier
        /// </summary>
        /// <param name="externalId">The entity external unique identifier</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>True if exists, otherwise false</returns>
        Task<bool> ExistsAsync(TId externalId, CancellationToken ct);
    }

    /// <summary>
    /// Represents the exists operation by an external unique identifier
    /// of <see cref="Guid"/> type.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface IExistsByExternalId<TEntity> : IExistsByExternalId<TEntity, Guid>
        where TEntity : class, IEntity, IHaveExternalId
    {

    }
}
