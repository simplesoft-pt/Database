using System;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents an entity with updated metadata properties
    /// </summary>
    /// <typeparam name="TBy">The updated by type</typeparam>
    public interface IHaveUpdatedMeta<TBy>
    {
        /// <summary>
        /// Date and time when the entity was most recently updated
        /// </summary>
        DateTimeOffset UpdatedOn { get; set; }

        /// <summary>
        /// User that updated the entity
        /// </summary>
        TBy UpdatedBy { get; set; }
    }

    /// <summary>
    /// Represents an entity with updated metadata properties with a string
    /// for the <see cref="IHaveUpdatedMeta{TBy}.UpdatedBy"/> property.
    /// </summary>
    public interface IHaveUpdatedMeta : IHaveUpdatedMeta<string>
    {

    }
}
