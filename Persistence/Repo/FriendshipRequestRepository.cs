﻿using Ardalis.Result;
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

        public async Task<Result<List<FriendshipRequest>>> GetIncomingFriendshipRequests(UserId userId, int skip, int take)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                DbSet<FriendshipRequest> requests = context.Set<FriendshipRequest>();
                List<FriendshipRequest> incoming = await requests.Where(x => x.FriendId == userId && !x.Accepted && !x.Rejected)
                                                                 .OrderBy(x => x.UserId)
                                                                 .Skip(skip)
                                                                 .Take(take)
                                                                 .ToListAsync();
                return Result.Success(incoming);
            }
        }

        public async Task<Result<bool>> CheckForPendingFriendshipRequestAsync(UserId from, UserId to)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                DbSet<FriendshipRequest> requests = context.Set<FriendshipRequest>();
                bool exists = await requests.AnyAsync(x => x.UserId == from && x.FriendId == to&&!x.Accepted&&!x.Deleted&&!x.Rejected);
                return Result.Success(exists);
            }
        }

        public async Task<Result<FriendshipRequest>> FindFriendRequestAsync(UserId from, UserId to)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                DbSet<FriendshipRequest> requests = context.Set<FriendshipRequest>();
                var found = await requests.SingleOrDefaultAsync(x => x.UserId == from && x.FriendId == to);
                if (found==null) return Result.NotFound();
                return Result.Success(found);
            }
        }

        public override Task<Result<FriendshipRequest>> GetByIdAsync(FriendshipRequestId id)
        {
            throw new NotImplementedException();
        }
    }
}
