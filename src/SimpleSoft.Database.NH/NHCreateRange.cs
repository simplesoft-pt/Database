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
    public class NHCreateRange<TEntity> : ICreateRange<TEntity>
        where TEntity : class, IEntity
    {
        private readonly NHSessionContainer _container;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="container"></param>
        public NHCreateRange(
            NHSessionContainer container
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
                foreach(var item in collection)
                    await ctx.SaveAsync(item, ct);

                return collection;
            }, entities as IReadOnlyCollection<TEntity> ?? entities.ToList(), ct);
        }
    }
}