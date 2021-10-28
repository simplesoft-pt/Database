using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the create operation in bulk
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EFCoreCreateRange<TEntity> : ICreateRange<TEntity>
        where TEntity : class, IEntity
    {
        private readonly EFCoreContextContainer _container;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="container"></param>
        public EFCoreCreateRange(
            EFCoreContextContainer container
        )
        {
            _container = container;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> CreateAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            return await _container.ExecuteAsync(async (ctx, collection, c) =>
            {
                await ctx.Set<TEntity>().AddRangeAsync(collection, c).ConfigureAwait(false);
                return collection;
            }, entities as IReadOnlyCollection<TEntity> ?? entities.ToList(), ct).ConfigureAwait(false);
        }
    }
}