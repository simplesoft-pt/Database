using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents an entity query
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public class EFCoreQueryable<TEntity> : IQueryable<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IQueryable<TEntity> _query;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="container"></param>
        public EFCoreQueryable(
            EFCoreContextContainer container
        )
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            _query = container.Query<TEntity>();
        }

        /// <inheritdoc />
        public IEnumerator<TEntity> GetEnumerator() => _query.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public Type ElementType => _query.ElementType;

        /// <inheritdoc />
        public Expression Expression => _query.Expression;

        /// <inheritdoc />
        public IQueryProvider Provider => _query.Provider;
    }
}
