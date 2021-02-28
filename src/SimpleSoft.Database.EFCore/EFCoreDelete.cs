using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SimpleSoft.Database
{
    /// <summary>
    /// Represents the delete operation
    /// </summary>
    /// <typeparam name="TContext">The context type</typeparam>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public class EFCoreDelete<TContext, TEntity> : IDelete<TEntity>
        where TContext : DbContext
        where TEntity : class, IEntity
    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> _set;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="context"></param>
        public EFCoreDelete(
            TContext context
        )
        {
            _context = context;
            _set = context.Set<TEntity>();
        }

        /// <inheritdoc />
        public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct)
        {
            var entry = _set.Remove(entity);
            await _context.SaveChangesAsync(ct);

            return entry.Entity;
        }
    }
}
