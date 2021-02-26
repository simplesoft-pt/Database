using System;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents an entity with created metadata properties
    /// </summary>
    /// <typeparam name="TBy">The created by type</typeparam>
    public interface IHaveCreatedMeta<TBy>
    {
        /// <summary>
        /// Date and time when the entity was created
        /// </summary>
        DateTimeOffset CreatedOn { get; set; }

        /// <summary>
        /// User that created the entity
        /// </summary>
        TBy CreatedBy { get; set; }
    }

    /// <summary>
    /// Represents an entity with created metadata properties with
    /// a string for the created by property
    /// </summary>
    public interface IHaveCreatedMeta : IHaveCreatedMeta<string>
    {

    }
}
