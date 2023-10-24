using Ardalis.Result;
using Domain.Entities;
using Domain.Errors;
using Domain.Repo;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repo
{
    public class UserRepository : GenericRepository<User, UserId>, IUserProfileRepository
    {
        private const int MIN_PAGESIZE = 1;
        private const int MAX_PAGESIZE = 100;
        private const int MIN_PAGE = 1;

        public UserRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory)
        {
        }

        /// <inheritdoc />
        public async Task<Result<bool>> IsInFriendlist(UserId userId, UserId friendId)
        {
            Result<User> userResult = await GetByIdAsync(userId);
            if (!userResult.IsSuccess) return DomainErrors.User.NotFound;
            if (userResult.Value.FriendsWith.Any(f => f.Id==friendId)
                || userResult.Value.FriendsTo.Any(f => f.Id==friendId))
                return Result.Success(true);
            else
                return Result.Success(false);
        }

        /// <inheritdoc />
        public async Task<Result<List<User>>> GetFriends(UserId userId)
        {
            Result<User> userResult = await GetByIdAsync(userId);
            if (!userResult.IsSuccess) return DomainErrors.User.NotFound;
            return Result.Success(userResult.Value.FriendsTo.Union(userResult.Value.FriendsWith).OrderBy(x => x.UserName).ToList());
        }

        /// <inheritdoc />
        public async Task<Result<User>> GetByIdentityAsync(string identityId)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                DbSet<User> users = context.Set<User>();
                User? result = await users.Include(x => x.FriendsTo)
                    .Include(x => x.FriendsWith)
                    .Include(x => x.Images)
                    .SingleOrDefaultAsync(x => x.AspUserIdentity == identityId);
                if (result == null) return DomainErrors.User.NotFound;
                return Result.Success(result);
            }
        }

        /// <inheritdoc />
        public async Task<Result<User>> GetByUserNameAsync(string userName)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                DbSet<User> users = context.Set<User>();
                User? foundUser = await users.Include(x => x.FriendsTo)
                    .Include(x => x.FriendsWith)
                    .Include(x => x.Images)
                    .SingleOrDefaultAsync(x => x.UserName == userName);
                if (foundUser == null) return DomainErrors.User.NotFound;
                return Result.Success(foundUser);
            }
        }

        /// <inheritdoc />
        public override async Task<Result<User>> GetByIdAsync(UserId id)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                DbSet<User> users = context.Set<User>();
                User? result = await users.Include(x => x.FriendsTo)
                    .Include(x => x.FriendsWith)
                    .Include(x => x.Images)
                    .SingleOrDefaultAsync(x => x.Id == id);
                if (result == null) return DomainErrors.User.NotFound;
                return Result.Success(result);
            }
        }
        /// <inheritdoc />
        public async Task<Result<List<User>>> GetUsersAsync(int page, int pageSize)
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
            if (page < MIN_PAGESIZE) errors.Add(new()
            {
                Identifier=nameof(page),
                ErrorMessage=$"Minimum page is {MIN_PAGESIZE}",
                Severity=ValidationSeverity.Error,
                ErrorCode="400"

            });
            if (pageSize < MIN_PAGE) errors.Add(new()
            {
                Identifier=nameof(page),
                ErrorMessage=$"Minimum page size is {MIN_PAGE}",
                Severity=ValidationSeverity.Error,
                ErrorCode="400"

            });
            if (pageSize > MAX_PAGESIZE) errors.Add(new()
            {
                Identifier=nameof(pageSize),
                ErrorMessage=$"Maximum page size is {MAX_PAGESIZE}",
                Severity=ValidationSeverity.Error,
                ErrorCode="400"
            });
            return errors;
        }

    }
}
