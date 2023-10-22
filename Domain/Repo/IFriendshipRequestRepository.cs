using Ardalis.Result;
using Domain.Entities;

namespace Domain.Repo
{
    public interface IFriendshipRequestRepository
    {
        /// <summary>
        /// Checks if the specified users have a pending friendship request.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="friend">The friend.</param>
        /// <returns>True if the specified users have a pending friendship request, otherwise false.</returns>
        Task<Result<bool>> CheckForPendingFriendshipRequestAsync(User user, User friend);

        Task<Result<FriendshipRequest>> GetByIdAsync(FriendshipRequestId id);
        Task<Result> InsertAsync(FriendshipRequest entity, CancellationToken cancellationToken = default);

    }
}
