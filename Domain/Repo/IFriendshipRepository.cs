using Domain.Entities;

namespace Domain.Repositories
{
    public interface IFriendshipRepository
    {
        /// <summary>
        /// Checks if the specified users are friends.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="friend">The friend.</param>
        /// <returns>True if the specified users are friends, otherwise false.</returns>
        Task<bool> CheckIfFriendsAsync(User user, User friend);


        Task<Friendship?> GetByIdAsync((UserId, UserId) id);

        Task<(UserId, UserId)> Save(Friendship entity, CancellationToken cancellationToken = default);
    }
}
