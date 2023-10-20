﻿using Domain.Entities;
using Domain.Repo;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repo
{

    public class FriendshipRequestRepository : GenericRepository<FriendshipRequest, FriendshipRequestId>, IFriendshipRequestRepository
    {
        public FriendshipRequestRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory)
        {
        }

        public Task<bool> CheckForPendingFriendshipRequestAsync(User user, User friend)
        {
            throw new NotImplementedException();
        }

        public override Task<FriendshipRequest?> GetByIdAsync(FriendshipRequestId id)
        {
            throw new NotImplementedException();
        }

        public override Task<FriendshipRequestId> Save(FriendshipRequest entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
