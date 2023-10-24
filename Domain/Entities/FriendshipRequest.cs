using Ardalis.GuardClauses;
using Ardalis.Result;
using Domain.Repo;

namespace Domain.Entities
{
    /// <summary>
    /// Represents the friendship request.
    /// </summary>
    public sealed class FriendshipRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FriendshipRequest"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="friend">The friend.</param>
        public FriendshipRequest(FriendshipRequestId id, User user, User friend)
        {
            Guard.Against.Default(id.Value, nameof(id), "The identifier is required.");
            Guard.Against.Null(user, nameof(user), "The user is required.");
            Guard.Against.Default(user.Id, $"{nameof(user)}{nameof(user.Id)}", "The user identifier is required.");
            Guard.Against.Null(friend, nameof(friend), "The friend is required.");
            Guard.Against.Default(friend.Id, $"{nameof(friend)}{nameof(friend.Id)}", "The friend identifier is required.");
            UserId = user.Id;
            FriendId = friend.Id;
            Id = id;
            FromUsername=user.UserName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FriendshipRequest"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        private FriendshipRequest()
        {
        }
        /// <summary>
        /// Gets the friendship request identifier.
        /// </summary>
        public FriendshipRequestId Id { get; private set; }
        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public UserId UserId { get; private set; }

        /// <summary>
        /// Gets the friend identifier.
        /// </summary>
        public UserId FriendId { get; private set; }
        /// <summary>
        /// Gets the from username.
        /// </summary>
        public string FromUsername { get; private set; }

        /// <summary>
        /// Gets the value indicating whether or not the friend request has been accepted.
        /// </summary>
        public bool Accepted { get; private set; }

        /// <summary>
        /// Gets the value indicating whether or not the friend request has been rejected.
        /// </summary>
        public bool Rejected { get; private set; }

        /// <inheritdoc />
        public DateTime? DeletedOnUtc { get; }

        /// <inheritdoc />
        public bool Deleted { get; }

        /// <summary>
        /// Accepts the friend request.
        /// </summary>
        /// <returns>The result of the accepting operation.</returns>
        public async Task<Result> Accept(IFriendshipRequestRepository friendshipRequestRepository)
        {
            if (Accepted)
            {
                return Result.Conflict("Friendship request already accepted");
            }

            if (Rejected)
            {
                return Result.Conflict("Friendship request already rejected");
            }
            Accepted = true;
            Result editResult = await friendshipRequestRepository.UpdateAsync(this);
            if (editResult.IsSuccess)
            {
                await friendshipRequestRepository.AddFriend(UserId, FriendId);
                return Result.Success();
            }
            return Result.Error("Error accepting friend request");
        }

        /// <summary>
        /// Rejects the friend request.
        /// </summary>
        /// <param name="utcNow">The current date and time in UTC format.</param>
        /// <returns>The result of the rejecting operation.</returns>
        public async Task<Result> Reject(IFriendshipRequestRepository friendshipRequestRepository)
        {
            if (Accepted)
            {
                return Result.Conflict("Friendship request already accepted");
            }

            if (Rejected)
            {
                return Result.Conflict("Friendship request already rejected");
            }

            Rejected = true;
            Result editResult = await friendshipRequestRepository.UpdateAsync(this);
            if (editResult.IsSuccess)
                return Result.Success();
            return Result.Error("Error rejecting friend request");
        }
    }
    public record FriendshipRequestId(Guid Value);
}
