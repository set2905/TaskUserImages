using Ardalis.GuardClauses;

namespace Domain.Entities
{
    public sealed class User
    {
        private readonly HashSet<Image> images = new();
        private readonly HashSet<User> friendsWith = new();
        private readonly HashSet<User> friendsTo = new();

        public User(UserId id, string userName)
        {
            Guard.Against.Default(id.Value, nameof(id), "The identifier is required.");
            Id=id;
            UserName=userName;  
        }

        private User() { }

        public UserId Id { get; private set; }
        public string UserName { get; private set; }
        public IReadOnlyCollection<Image> Images => images;
        public IReadOnlyCollection<User> FriendsWith => friendsWith;
        public IReadOnlyCollection<User> FriendsTo => friendsTo;

    }
    public record UserId(Guid Value);
}
