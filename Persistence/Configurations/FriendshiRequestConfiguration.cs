using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations
{
    /// <summary>
    /// Represents the configuration for the <see cref="FriendshipRequest"/> entity.
    /// </summary>
    internal sealed class FriendshipRequestConfiguration : IEntityTypeConfiguration<FriendshipRequest>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<FriendshipRequest> builder)
        {
            builder.HasKey(friendshipRequest => friendshipRequest.Id);
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(friendshipRequest => friendshipRequest.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(friendshipRequest => friendshipRequest.FriendId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(f => f.Id).HasConversion(id => id.Value,
                                 val => new(val));
            builder.Property(f => f.UserId).HasConversion(id => id.Value,
                                 val => new(val));
            builder.Property(f => f.FriendId).HasConversion(id => id.Value,
                                             val => new(val));

            builder.Property(friendshipRequest => friendshipRequest.Accepted).HasDefaultValue(false);

            builder.Property(friendshipRequest => friendshipRequest.Rejected).HasDefaultValue(false);

            builder.Property(friendshipRequest => friendshipRequest.DeletedOnUtc);
            builder.Property(friendshipRequest => friendshipRequest.FromUsername).HasColumnName("FromUsername");

            builder.Property(friendshipRequest => friendshipRequest.Deleted).HasDefaultValue(false);

            builder.HasQueryFilter(friendshipRequest => !friendshipRequest.Deleted);
        }
    }
}
