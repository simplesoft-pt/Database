﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the delete operation in bulk
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EFCoreUpdateRange<TEntity> : IUpdateRange<TEntity>
        where TEntity : class, IEntity
    {
        private readonly EFCoreContextContainer _container;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="container"></param>
        public EFCoreUpdateRange(
            EFCoreContextContainer container
        )
        {
            _container = container;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            return await _container.ExecuteAsync((ctx, collection, c) =>
            {
                ctx.Set<TEntity>().UpdateRange(collection);
                return Task.FromResult(collection);
            }, entities as IReadOnlyCollection<TEntity> ?? entities.ToList(), ct).ConfigureAwait(false);
        }
    }
}