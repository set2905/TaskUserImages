using Ardalis.Result;
using Domain.Entities;
using Domain.Repo;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repo
{

    public class FriendshipRequestRepository : GenericRepository<FriendshipRequest, FriendshipRequestId>, IFriendshipRequestRepository
    {
        public FriendshipRequestRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory)
        {
        }

        public Task<Result<bool>> CheckForPendingFriendshipRequestAsync(User from, User to)
        {
            throw new NotImplementedException();
        }

        public override Task<Result<FriendshipRequest>> GetByIdAsync(FriendshipRequestId id)
        {
            throw new NotImplementedException();
        }

        public override Task<Result> Insert(FriendshipRequest entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
