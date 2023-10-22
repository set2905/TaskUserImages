using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
        public virtual async Task<Result> Delete(TEntity entity, CancellationToken cancellationToken = default)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                context.Set<TEntity>().Remove(entity);
                await context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
        }
        /// <summary>
        /// Gets the entity with the specified identifier.
        /// </summary>
        /// <param name="id">The entity identifier.</param>
        public abstract Task<Result<TEntity>> GetByIdAsync(TId id);

        public virtual async Task<Result> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                context.Entry(entity).State = EntityState.Added;
                await context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
        }
    }
}
