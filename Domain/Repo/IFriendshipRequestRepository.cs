﻿using Ardalis.Result;
using Domain.Entities;

namespace Domain.Repo
{
    public interface IFriendshipRequestRepository
    {
        /// <summary>
        /// Checks if the specified users have a pending friendship request.
        /// </summary>
        /// <param name="user">The user id.</param>
        /// <param name="friend">The friend id.</param>
        /// <returns>True if the specified users have a pending friendship request, otherwise false.</returns>
        Task<Result<bool>> CheckForPendingFriendshipRequestAsync(UserId user, UserId friend);
        Task<Result<FriendshipRequest>> FindFriendRequest(UserId from, UserId to);
        Task<Result<FriendshipRequest>> GetByIdAsync(FriendshipRequestId id);
        Task<Result<List<FriendshipRequest>>> GetIncomingFriendshipRequests(UserId userId, int skip, int take);
        Task<Result> InsertAsync(FriendshipRequest entity, CancellationToken cancellationToken = default);

    }
}
