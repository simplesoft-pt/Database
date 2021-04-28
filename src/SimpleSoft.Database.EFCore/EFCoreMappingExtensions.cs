using System;
using SimpleSoft.Database;

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore.Metadata.Builders
{
    /// <summary>
    /// Extensions for <see cref="EntityTypeBuilder{TEntity}"/> instances.
    /// </summary>
    public static class EFCoreMappingExtensions
    {
        /// <summary>
        /// Maps the <see cref="IEntity{TId}.Id"/> property as a primary key for the table.
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TId">The id type</typeparam>
        /// <param name="builder">Entity model builder</param>
        /// <param name="generatedOnAdd">If true, the primary key will be generated when added, otherwise it must be defined before</param>
        /// <returns></returns>
        public static EntityTypeBuilder<T> MapPrimaryKey<T, TId>(this EntityTypeBuilder<T> builder, bool generatedOnAdd = true)
            where T : class, IEntity<TId> 
            where TId : IEquatable<TId>
        {
            builder.HasKey(e => e.Id);

            var idBuilder = builder
                .Property(e => e.Id)
                .IsRequired();

            if (generatedOnAdd)
                idBuilder.ValueGeneratedOnAdd();
            else
                idBuilder.ValueGeneratedNever();

            return builder;
        }

        /// <summary>
        /// Maps the <see cref="IEntity{TId}.Id"/> property as a primary key for the table.
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="builder">Entity model builder</param>
        /// <param name="generatedOnAdd">If true, the primary key will be generated when added, otherwise it must be defined before</param>
        /// <returns></returns>
        public static EntityTypeBuilder<T> MapPrimaryKey<T>(this EntityTypeBuilder<T> builder, bool generatedOnAdd = true)
            where T : class, IEntity<long>
        {
            return builder.MapPrimaryKey<T, long>(generatedOnAdd);
        }

        /// <summary>
        /// Maps the <see cref="IHaveExternalId{TId}.ExternalId"/> property as a required alternate key or unique index for the table.
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TId">The id type</typeparam>
        /// <param name="builder">Entity model builder</param>
        /// <param name="isAlternateKey">If true, alternate key will be used, otherwise unique index</param>
        /// <returns></returns>
        public static EntityTypeBuilder<T> MapExternalId<T, TId>(this EntityTypeBuilder<T> builder, bool isAlternateKey = true)
            where T : class, IHaveExternalId<TId> 
            where TId : IEquatable<TId>
        {
            if (isAlternateKey)
                builder.HasAlternateKey(e => e.ExternalId);
            else
                builder.HasIndex(e => e.ExternalId).IsUnique();

            builder.Property(e => e.ExternalId)
                .IsRequired();

            return builder;
        }

        /// <summary>
        /// Maps the <see cref="IHaveExternalId{TId}.ExternalId"/> property as a required alternate key or unique index for the table.
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="builder">Entity model builder</param>
        /// <param name="isAlternateKey">If true, alternate key will be used, otherwise unique index</param>
        /// <returns></returns>
        public static EntityTypeBuilder<T> MapExternalId<T>(this EntityTypeBuilder<T> builder, bool isAlternateKey = true)
            where T : class, IHaveExternalId
        {
            return builder.MapExternalId<T, Guid>(isAlternateKey);
        }
    }
}
