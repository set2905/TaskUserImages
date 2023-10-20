using Microsoft.EntityFrameworkCore;

namespace Persistence.Repo
{
    public abstract class GenericRepository<TEntity, TId> where TEntity : class
    {
        /// <summary>
        /// Gets the database context factory.
        /// </summary>
        protected readonly IDbContextFactory<ApplicationDbContext> contextFactory;
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{TEntity}"/> class.
        /// </summary>
        protected GenericRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory=contextFactory;
        }
        public virtual async Task<bool> Delete(TEntity entity, CancellationToken cancellationToken = default)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                context.Set<TEntity>().Remove(entity);
                await context.SaveChangesAsync(cancellationToken);
                return true;
            }
        }
        /// <summary>
        /// Gets the entity with the specified identifier.
        /// </summary>
        /// <param name="id">The entity identifier.</param>
        public abstract Task<TEntity?> GetByIdAsync(TId id);

        public abstract Task<TId> Save(TEntity entity, CancellationToken cancellationToken = default);
    }
}
