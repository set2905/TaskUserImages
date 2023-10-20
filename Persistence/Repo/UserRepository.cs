using Ardalis.Result;
using Domain.Entities;
using Domain.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Xml.Linq;

namespace Persistence.Repo
{
    public class UserRepository : GenericRepository<User, UserId>, IUserRepository
    {
        public UserRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory)
        {
        }

        public override Task<Result<User>> GetByIdAsync(UserId id)
        {
            throw new NotImplementedException();
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
    }
}
