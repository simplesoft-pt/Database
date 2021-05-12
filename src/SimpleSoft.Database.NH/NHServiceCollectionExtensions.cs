using System;
using System.Linq;
using SimpleSoft.Database;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/> instances.
    /// </summary>
    public static class NHServiceCollectionExtensions
    {
        /// <summary>
        /// Registers into the container wrappers for each context operation, enabling
        /// the usage of interfaces like <see cref="IReadByExternalId{TEntity}"/>.
        /// </summary>
        /// <remarks>
        /// Services are added using variations of `TryAdd...`, ensuring you can swap
        /// particular interface implementations by your own either by adding before or after
        /// using the <see cref="ServiceCollectionDescriptorExtensions.Replace"/> method.
        /// </remarks>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException" />
        public static IServiceCollection AddSessionOperations(
            this IServiceCollection services,
            Action<NHSessionContainerOptions> config = null
        )
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            if (config != null)
                services.Configure(config);

            services.TryAddTransient<NHSessionContainer>();

            services.TryAddSingleton<IUnitOfWorkFactory, NHUnitOfWorkFactory>();
            services.TryAddTransient<IUnitOfWork, NHUnitOfWork>();
            services.TryAddScoped<ITransaction, NHTransaction>();

            // CREATE
            services.TryAddTransient(typeof(ICreate<>), typeof(NHCreate<>));
            services.TryAddTransient(typeof(ICreateRange<>), typeof(NHCreateRange<>));

            // READ
            services.TryAddTransient(typeof(IReadByExternalId<,>), typeof(NHReadByExternalId<,>));
            services.TryAddTransient(typeof(IReadByExternalId<>), typeof(NHReadByExternalId<>));
            services.TryAddTransient(typeof(IReadByExternalIdRange<,>), typeof(NHReadByExternalIdRange<,>));
            services.TryAddTransient(typeof(IReadByExternalIdRange<>), typeof(NHReadByExternalIdRange<>));
            services.TryAddTransient(typeof(IReadById<,>), typeof(NHReadById<,>));
            services.TryAddTransient(typeof(IReadById<>), typeof(NHReadById<>));
            services.TryAddTransient(typeof(IReadByIdRange<,>), typeof(NHReadByIdRange<,>));
            services.TryAddTransient(typeof(IReadByIdRange<>), typeof(NHReadByIdRange<>));

            // UPDATE
            services.TryAddTransient(typeof(IUpdate<>), typeof(NHUpdate<>));
            services.TryAddTransient(typeof(IUpdateRange<>), typeof(NHUpdateRange<>));

            // DELETE
            services.TryAddTransient(typeof(IDelete<>), typeof(NHDelete<>));
            services.TryAddTransient(typeof(IDeleteRange<>), typeof(NHDeleteRange<>));

            // EXISTS
            services.TryAddTransient(typeof(IExistsByExternalId<,>), typeof(NHExistsByExternalId<,>));
            services.TryAddTransient(typeof(IExistsByExternalId<>), typeof(NHExistsByExternalId<>));
            services.TryAddTransient(typeof(IExistsById<,>), typeof(NHExistsById<,>));
            services.TryAddTransient(typeof(IExistsById<>), typeof(NHExistsById<>));

            // QUERY
            services.TryAddTransient(typeof(IQueryable<>), typeof(NHQueryable<>));

            return services;
        }
    }
}
