using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using SimpleSoft.Database;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/> instances.
    /// </summary>
    public static class ServiceCollectionExtensions
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
        /// <typeparam name="TContext">The context type</typeparam>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException" />
        public static IServiceCollection AddDbContextOperations<TContext>(
            this IServiceCollection services,
            Action<EFCoreContextContainerOptions> config = null
        ) where TContext : DbContext
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            if (config != null)
                services.Configure(config);

            services.TryAddTransient(s => new EFCoreContextContainer(
                s.GetRequiredService<TContext>(),
                s.GetRequiredService<IOptions<EFCoreContextContainerOptions>>()
            ));

            services.TryAddTransient<ITransaction, EFCoreTransaction>();

            // CREATE
            services.TryAddTransient(typeof(ICreate<>), typeof(EFCoreCreate<>));
            services.TryAddTransient(typeof(ICreateRange<>), typeof(EFCoreCreateRange<>));

            // READ
            services.TryAddTransient(typeof(IReadByExternalId<,>), typeof(EFCoreReadByExternalId<,>));
            services.TryAddTransient(typeof(IReadByExternalId<>), typeof(EFCoreReadByExternalId<>));
            services.TryAddTransient(typeof(IReadByExternalIdRange<,>), typeof(EFCoreReadByExternalIdRange<,>));
            services.TryAddTransient(typeof(IReadByExternalIdRange<>), typeof(EFCoreReadByExternalIdRange<>));
            services.TryAddTransient(typeof(IReadById<,>), typeof(EFCoreReadById<,>));
            services.TryAddTransient(typeof(IReadById<>), typeof(EFCoreReadById<>));
            services.TryAddTransient(typeof(IReadByIdRange<,>), typeof(EFCoreReadByIdRange<,>));
            services.TryAddTransient(typeof(IReadByIdRange<>), typeof(EFCoreReadByIdRange<>));

            // UPDATE
            services.TryAddTransient(typeof(IUpdate<>), typeof(EFCoreUpdate<>));
            services.TryAddTransient(typeof(IUpdateRange<>), typeof(EFCoreUpdateRange<>));

            // DELETE
            services.TryAddTransient(typeof(IDelete<>), typeof(EFCoreDelete<>));
            services.TryAddTransient(typeof(IDeleteRange<>), typeof(EFCoreDeleteRange<>));

            // EXISTS
            services.TryAddTransient(typeof(IExistsByExternalId<,>), typeof(EFCoreExistsByExternalId<,>));
            services.TryAddTransient(typeof(IExistsByExternalId<>), typeof(EFCoreExistsByExternalId<>));
            services.TryAddTransient(typeof(IExistsById<,>), typeof(EFCoreExistsById<,>));
            services.TryAddTransient(typeof(IExistsById<>), typeof(EFCoreExistsById<>));

            // QUERY
            services.TryAddTransient(typeof(IQueryable<>), typeof(EFCoreQueryable<>));

            return services;
        }
    }
}
