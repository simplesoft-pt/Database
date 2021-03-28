using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the create operation in bulk
    /// </summary>
    /// <typeparam name="TContext">The context type</typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class EFCoreCreateRange<TContext, TEntity> : ICreateRange<TEntity>
        where TContext : DbContext
        where TEntity : class, IEntity
    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> _set;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="context"></param>
        public EFCoreCreateRange(
            TContext context
        )
        {
            _context = context;
            _set = context.Set<TEntity>();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> CreateAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            var enumeratedEntities = entities as IReadOnlyCollection<TEntity> ?? entities.ToList();

            await _set.AddRangeAsync(enumeratedEntities, ct);
            await _context.SaveChangesAsync(ct);

            return enumeratedEntities;
        }
    }
}