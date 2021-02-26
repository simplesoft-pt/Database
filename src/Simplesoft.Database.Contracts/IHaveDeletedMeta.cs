using System;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents an entity with soft delete metadata properties
    /// </summary>
    /// <typeparam name="TBy">The updated by type</typeparam>
    public interface IHaveDeletedMeta<TBy>
    {
        /// <summary>
        /// Date and time when the entity was most recently updated
        /// </summary>
        DateTimeOffset? DeletedOn { get; set; }

        /// <summary>
        /// User that updated the entity
        /// </summary>
        TBy DeletedBy { get; set; }
    }

    /// <summary>
    /// Represents an entity with soft delete metadata properties with a string
    /// for the <see cref="IHaveDeletedMeta{TBy}.DeletedBy"/> property.
    /// </summary>
    public interface IHaveDeletedMeta : IHaveDeletedMeta<string>
    {

    }
}
