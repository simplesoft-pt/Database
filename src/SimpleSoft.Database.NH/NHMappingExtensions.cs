using NHibernate.Mapping.ByCode.Conformist;
using System;
using NHibernate.Mapping.ByCode;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Extensions for <see cref="ClassMapping{TEntity}"/> instances.
    /// </summary>
    public static class NHMappingExtensions
    {
        /// <summary>
        /// Maps the <see cref="IEntity{TId}.Id"/> property as a primary key for the table.
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TId">The id type</typeparam>
        /// <param name="builder">Entity model builder</param>
        /// <param name="generatedOnAdd">If true, the primary key will be generated when added, otherwise it must be defined before</param>
        /// <returns></returns>
        public static ClassMapping<T> MapPrimaryKey<T, TId>(this ClassMapping<T> builder, bool generatedOnAdd = true)
            where T : class, IEntity<TId>
            where TId : IEquatable<TId>
        {
            var generator = generatedOnAdd ? Generators.Identity : Generators.Assigned;

            builder.Id(e => e.Id, mapper => mapper.Generator(generator));

            return builder;
        }

        /// <summary>
        /// Maps the <see cref="IEntity{TId}.Id"/> property as a primary key for the table.
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="builder">Entity model builder</param>
        /// <param name="generatedOnAdd">If true, the primary key will be generated when added, otherwise it must be defined before</param>
        /// <returns></returns>
        public static ClassMapping<T> MapPrimaryKey<T>(this ClassMapping<T> builder, bool generatedOnAdd = true)
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
        public static ClassMapping<T> MapExternalId<T, TId>(this ClassMapping<T> builder, bool isAlternateKey = true)
            where T : class, IHaveExternalId<TId>
            where TId : IEquatable<TId>
        {
            if (isAlternateKey)
                builder.NaturalId(m => m.Property(m => m.ExternalId));
            else
                builder.Property(e => e.ExternalId, map =>
                {
                    map.Index(nameof(IHaveExternalId<TId>.ExternalId));
                    map.UniqueKey(nameof(IHaveExternalId<TId>.ExternalId));
                });

            builder.Property(e => e.ExternalId, m => m.NotNullable(true));

            return builder;
        }

        /// <summary>
        /// Maps the <see cref="IHaveExternalId{TId}.ExternalId"/> property as a required alternate key or unique index for the table.
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="builder">Entity model builder</param>
        /// <param name="isAlternateKey">If true, alternate key will be used, otherwise unique index</param>
        /// <returns></returns>
        public static ClassMapping<T> MapExternalId<T>(this ClassMapping<T> builder, bool isAlternateKey = true)
            where T : class, IHaveExternalId
        {
            return builder.MapExternalId<T, Guid>(isAlternateKey);
        }
    }
}
