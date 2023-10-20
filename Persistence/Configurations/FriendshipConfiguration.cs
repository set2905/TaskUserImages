using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.HasKey(friendship => new
            {
                friendship.UserId,
                friendship.FriendId
            });
            builder.Property(i => i.UserId).HasConversion(uId => uId.Value,
                                                         val => new(val));
            builder.Property(i => i.FriendId).HasConversion(fId => fId.Value,
                                                         val => new(val));

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(friendship => friendship.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(friendship => friendship.FriendId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(friendship => friendship.CreatedOnUtc).IsRequired();
        }
    }
}
