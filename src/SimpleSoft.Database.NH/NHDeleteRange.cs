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
    public class NHDeleteRange<TEntity> : IDeleteRange<TEntity>
        where TEntity : class, IEntity
    {
        private readonly NHSessionContainer _container;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="container"></param>
        public NHDeleteRange(
            NHSessionContainer container
        )
        {
            _container = container;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            return await _container.ExecuteAsync(async (ctx, collection, c) =>
            {
                foreach (var item in collection)
                    await ctx.DeleteAsync(item, ct).ConfigureAwait(false);

                return collection;
            }, entities as IReadOnlyCollection<TEntity> ?? entities.ToList(), ct).ConfigureAwait(false);
        }
    }
}