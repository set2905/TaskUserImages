using Ardalis.GuardClauses;

namespace Domain.Entities
{
    public sealed class User
    {
        private readonly HashSet<Image> images = new();

        public User(UserId id, string userName)
        {
            Guard.Against.Default(id.Value, nameof(id), "The identifier is required.");
            Id=id;
            UserName=userName;  
        }

        private User() { }

        public UserId Id { get; private set; }
        public string UserName { get; private set; }
        public IReadOnlyList<Image> Images => images.ToList();

    }
    public record UserId(Guid Value);
}
