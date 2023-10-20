using Domain.Entities;
using Domain.Repo;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repo
{
    public class UserRepository : GenericRepository<User, UserId>, IUserRepository
    {
        public UserRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory)
        {
        }

        public override Task<User?> GetByIdAsync(UserId id)
        {
            throw new NotImplementedException();
        }

        public override Task<UserId> Save(User entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
