using Ardalis.GuardClauses;
using System.Security.Cryptography;

namespace Domain.Entities
{
    public sealed class Image
    {
        public Image(ImageId id, UserId userId, string fileName)
        {
            Guard.Against.Default(id.Value, nameof(id), "The identifier is required.");
            Guard.Against.Default(userId.Value, nameof(userId), "The user id is required.");
            Guard.Against.NullOrEmpty(fileName, nameof(fileName), "The file name is required.");
            Id=id;
            UserId=userId;
            FileName=fileName;

            var randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                Key = Convert.ToBase64String(randomNumber);
            }
        }
        public ImageId Id { get; private set; }
        public UserId UserId { get; private set; }
        public string FileName { get; private set; }
        public string Key { get; private set; }
        private Image() { }
    }
    public record ImageId(Guid Value);
}
