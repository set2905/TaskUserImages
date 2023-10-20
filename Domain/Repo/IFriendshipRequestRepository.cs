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
        Task<bool> CheckForPendingFriendshipRequestAsync(User user, User friend);

        Task<FriendshipRequest?> GetByIdAsync(FriendshipRequestId id);
        Task<FriendshipRequestId> Save(FriendshipRequest entity, CancellationToken cancellationToken = default);

    }
}
