using Ardalis.GuardClauses;

namespace Domain.Entities
{
    /// <summary>
    /// Represents the friendship.
    /// </summary>
    public sealed class Friendship
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Friendship"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="friend">The friend.</param>
        public Friendship(User user, User friend)
        {
            Guard.Against.Null(user, nameof(user), "The user is required.");
            Guard.Against.Default(user.Id.Value, $"{nameof(user)}{nameof(user.Id)}", "The user identifier is required.");
            Guard.Against.Null(friend, nameof(friend), "The friend is required.");
            Guard.Against.Default(friend.Id.Value, $"{nameof(friend)}{nameof(friend.Id)}", "The friend identifier is required.");

            UserId = user.Id;
            FriendId = friend.Id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Friendship"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        private Friendship()
        {
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public UserId UserId { get; private set; }

        /// <summary>
        /// Gets the friend identifier.
        /// </summary>
        public UserId FriendId { get; private set; }

        public DateTime CreatedOnUtc { get; }

    }
}
