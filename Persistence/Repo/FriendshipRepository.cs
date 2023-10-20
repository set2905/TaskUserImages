using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repo
{
    public class FriendshipRepository : GenericRepository<Friendship, (UserId, UserId)>, IFriendshipRepository
    {
        public FriendshipRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory)
        {
        }

        public Task<bool> CheckIfFriendsAsync(User user, User friend)
        {
            throw new NotImplementedException();
        }

        public override Task<Friendship?> GetByIdAsync((UserId, UserId) id)
        {
            throw new NotImplementedException();
        }

        public override Task<(UserId, UserId)> Save(Friendship entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
