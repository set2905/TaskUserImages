using Ardalis.Result;
using Domain.Entities;
using Domain.Repo;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repo
{
    public class UserRepository : GenericRepository<User, UserId>, IUserRepository
    {
        private const int MIN_PAGESIZE = 1;
        private const int MAX_PAGESIZE = 100;
        private const int MIN_PAGE = 1;

        public UserRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory)
        {
        }

        public override async Task<Result<User>> GetByIdAsync(UserId id)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                DbSet<User> users = context.Set<User>();
                User? result = await users.SingleOrDefaultAsync(x => x.Id == id);
                if (result == null) return Result.NotFound($"User with id {id.Value} is not found");
                return Result.Success(result);
            }
        }

        public override async Task<Result> Insert(User entity, CancellationToken cancellationToken = default)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                context.Entry(entity).State = EntityState.Added;
                await context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
        }
        public async Task<Result<List<User>>> GetUsers(int page, int pageSize)
        {
            var errors = GetValidationErrors(page, pageSize);
            if (errors.Count>0) return Result.Invalid(errors);

            using (var context = contextFactory.CreateDbContext())
            {
                var users = context.Set<User>();
                List<User> result = await users.OrderBy(x => x.UserName).Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
                return Result.Success(result);
            }
        }
        private List<ValidationError> GetValidationErrors(int page, int pageSize)
        {
            List<ValidationError> errors = new();
            if (page < MIN_PAGESIZE) errors.Add(new() { ErrorMessage=$"Minimum page is {MIN_PAGESIZE}" });
            if (pageSize < MIN_PAGE) errors.Add(new() { ErrorMessage=$"Minimum page size is {MIN_PAGE}" });
            if (pageSize > MAX_PAGESIZE) errors.Add(new() { ErrorMessage=$"Maximum page size is {MAX_PAGESIZE}" });
            return errors;
        }

    }
}
