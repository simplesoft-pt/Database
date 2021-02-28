using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the delete operation in bulk
    /// </summary>
    /// <typeparam name="TContext">The context type</typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class EFCoreDeleteRange<TContext, TEntity> : IDeleteRange<TEntity>
        where TContext : DbContext
        where TEntity : class, IEntity
    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> _set;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="context"></param>
        public EFCoreDeleteRange(
            TContext context
        )
        {
            _context = context;
            _set = context.Set<TEntity>();
        }

        /// <inheritdoc />
        public Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken ct) =>
            DeleteAsync(entities as IList<TEntity> ?? entities.ToList(), ct);

        /// <inheritdoc />
        public Task<IEnumerable<TEntity>> DeleteAsync(CancellationToken ct, params TEntity[] entities) =>
            DeleteAsync(entities, ct);

        private async Task<IEnumerable<TEntity>> DeleteAsync(IList<TEntity> entities, CancellationToken ct)
        {
            _set.RemoveRange(entities);
            await _context.SaveChangesAsync(ct);

            return entities;
        }
    }
}