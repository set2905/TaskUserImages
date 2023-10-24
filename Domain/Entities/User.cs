using Ardalis.GuardClauses;
using Ardalis.Result;

namespace Domain.Entities
{
    public sealed class User
    {
        private readonly HashSet<Image> images = new();
        private readonly HashSet<User> friendsWith = new();
        private readonly HashSet<User> friendsTo = new();

        public User(UserId id, string userName, string identityId)
        {
            Guard.Against.Default(id.Value, nameof(id), "The identifier is required.");
            Id=id;
            UserName=userName;
            AspUserIdentity = identityId;
        }

        private User() { }

        public UserId Id { get; private set; }
        public string UserName { get; private set; }
        public string AspUserIdentity { get; private set; }
        public IReadOnlyCollection<Image> Images => images;
        public IReadOnlyCollection<User> FriendsWith => friendsWith;
        public IReadOnlyCollection<User> FriendsTo => friendsTo;
    }
    public record UserId(Guid Value);
}
