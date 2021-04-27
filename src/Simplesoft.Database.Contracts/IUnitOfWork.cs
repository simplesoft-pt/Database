using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Interface representing an unit of work.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Creates an entity
        /// </summary>
        /// <param name="entity">The entity to create</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The entity after changes</returns>
        Task<TEntity> CreateAsync<TEntity>(TEntity entity, CancellationToken ct)
           where TEntity : class, IEntity;

        /// <summary>
        /// Creates a range of entities
        /// </summary>
        /// <param name="entities">The entity collection</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The entity collection after changes</returns>
        Task<IEnumerable<TEntity>> CreateAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken ct)
            where TEntity : class, IEntity;

        /// <summary>
        /// Read an entity by a unique identifier, returning null if not found.
        /// </summary>
        /// <param name="externalId">The entity external unique identifier</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The entity or null if not found</returns>
        Task<TEntity> ReadByExternalIdAsync<TEntity, TId>(TId externalId, CancellationToken ct)
            where TEntity : class, IEntity, IHaveExternalId<TId>
            where TId : IEquatable<TId>;

        /// <summary>
        /// Represents the read operation by an external unique identifier
        /// of <see cref="Guid"/> type.
        /// </summary>
        /// <typeparam name="TEntity">The entity type</typeparam>
        Task<TEntity> ReadByExternalIdAsync<TEntity>(Guid externalId, CancellationToken ct)
            where TEntity : class, IEntity, IHaveExternalId;

        /// <summary>
        /// Reads a collection of entities by their external unique identifiers.
        /// </summary>
        /// <param name="externalIds">The collection of external unique identifiers</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The collection of entities</returns>
        Task<IEnumerable<TEntity>> ReadByExternalIdAsync<TEntity, TId>(IEnumerable<TId> externalIds, CancellationToken ct)
            where TEntity : class, IEntity, IHaveExternalId<TId>
            where TId : IEquatable<TId>;

        /// <summary>
        /// Represents the read bulk operation by a collection of
        /// external unique identifiers of <see cref="Guid"/> type.
        /// </summary>
        /// <typeparam name="TEntity">The entity type</typeparam>
        Task<IEnumerable<TEntity>> ReadByExternalIdAsync<TEntity>(IEnumerable<Guid> externalIds, CancellationToken ct)
            where TEntity : class, IEntity, IHaveExternalId;

        /// <summary>
        /// Read an entity by a unique identifier, returning null if not found.
        /// </summary>
        /// <param name="id">The entity id</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The entity or null if not found</returns>
        Task<TEntity> ReadByIdAsync<TEntity, TId>(TId id, CancellationToken ct)
            where TEntity : class, IEntity<TId>
            where TId : IEquatable<TId>;

        /// <summary>
        /// Represents the read operation by a long unique identifier
        /// </summary>
        /// <typeparam name="TEntity">The entity type</typeparam>
        Task<TEntity> ReadByIdAsync<TEntity>(long id, CancellationToken ct)
            where TEntity : class, IEntity<long>;

        /// <summary>
        /// Reads a collection of entities by their unique identifiers.
        /// </summary>
        /// <param name="ids">The collection of ids</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The collection of entities</returns>
        Task<IEnumerable<TEntity>> ReadByIdAsync<TEntity, TId>(IEnumerable<TId> ids, CancellationToken ct)
            where TEntity : class, IEntity<TId>
            where TId : IEquatable<TId>;

        /// <summary>
        /// Represents the read bulk operation by a collection of
        /// unique identifiers of <see cref="long"/> type.
        /// </summary>
        /// <typeparam name="TEntity">The entity type</typeparam>
        Task<IEnumerable<TEntity>> ReadByIdAsync<TEntity>(IEnumerable<long> ids, CancellationToken ct)
            where TEntity : class, IEntity<long>;

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The entity after changes</returns>
        Task<TEntity> UpdateAsync<TEntity>(TEntity entity, CancellationToken ct)
            where TEntity : class, IEntity;

        /// <summary>
        /// Updates a range of entities
        /// </summary>
        /// <param name="entities">The entity collection</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The entity collection after changes</returns>
        Task<IEnumerable<TEntity>> UpdateAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken ct)
            where TEntity : class, IEntity;

        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The entity after changes</returns>
        Task<TEntity> DeleteAsync<TEntity>(TEntity entity, CancellationToken ct)
            where TEntity : class, IEntity;

        /// <summary>
        /// Deletes a range of entities
        /// </summary>
        /// <param name="entities">The entity collection</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The entity collection after changes</returns>
        Task<IEnumerable<TEntity>> DeleteAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken ct)
            where TEntity : class, IEntity;

        /// <summary>
        /// Checks if an entity exists for a given unique identifier
        /// </summary>
        /// <param name="externalId">The entity external unique identifier</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>True if exists, otherwise false</returns>
        Task<bool> ExistsByExternalIdAsync<TEntity, TId>(TId externalId, CancellationToken ct)
            where TEntity : class, IEntity, IHaveExternalId<TId>
            where TId : IEquatable<TId>;

        /// <summary>
        /// Represents the exists operation by an external unique identifier
        /// of <see cref="Guid"/> type.
        /// </summary>
        /// <typeparam name="TEntity">The entity type</typeparam>
        Task<bool> ExistsByExternalIdAsync<TEntity>(Guid externalId, CancellationToken ct)
            where TEntity : class, IEntity, IHaveExternalId;

        /// <summary>
        /// Checks if an entity exists for a given unique identifier
        /// </summary>
        /// <param name="id">The entity id</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>True if exists, otherwise false</returns>
        Task<bool> ExistsByIdAsync<TEntity, TId>(TId id, CancellationToken ct)
            where TEntity : class, IEntity<TId>
            where TId : IEquatable<TId>;

        /// <summary>
        /// Represents the exists operation by an unique identifier
        /// of <see cref="long"/> type.
        /// </summary>
        /// <typeparam name="TEntity">The entity type</typeparam>
        Task<bool> ExistsByIdAsync<TEntity>(long id, CancellationToken ct)
            where TEntity : class, IEntity<long>;

        /// <summary>
        /// Gets a collection of elements
        /// </summary>
        /// <returns>The entity collection</returns>
        IQueryable<TEntity> Query<TEntity>()
            where TEntity : class, IEntity;

        /// <summary>
        /// Begin a transaction />
        /// </summary>
        /// <param name="ct"></param>
        /// <returns>A completed task of this operation</returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task<ITransaction> BeginTransactionAsync(CancellationToken ct);
    }
}
