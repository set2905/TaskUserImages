using Azure;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Hosting;

namespace Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasConversion(id => id.Value,
                                                          val => new(val));

            builder.HasMany(u=>u.Images)
                   .WithOne()
                   .HasForeignKey(img => img.UserId)
                   .IsRequired();

            builder.HasMany(u => u.FriendsTo)
                   .WithMany(u => u.FriendsWith)
                   .UsingEntity<Friendship>(
                        l => l.HasOne<User>().WithMany().HasForeignKey(e => e.UserId),
                        r => r.HasOne<User>().WithMany().HasForeignKey(e => e.FriendId)); 

            builder.HasIndex(u => u.UserName)
                   .IsUnique();
            builder.HasIndex(u => u.AspUserIdentity)
                   .IsUnique();
        }
    }
}
