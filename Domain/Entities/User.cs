using Ardalis.GuardClauses;

namespace Domain.Entities
{
    public sealed class User
    {
        private readonly HashSet<Image> images = new();

        public User(UserId id)
        {
            Guard.Against.Default(id.Value, nameof(id), "The identifier is required.");
            Id=id;
        }

        private User() { }

        public UserId Id { get; private set; }
        public IReadOnlyList<Image> Images => images.ToList();

    }
    public record UserId(Guid Value);
}
