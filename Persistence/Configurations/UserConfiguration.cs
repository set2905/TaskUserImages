using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasConversion(id => id.Value,
                                                          val => new(val));
            builder.HasMany(u=>u.Images)
                   .WithOne()
                   .HasForeignKey(img => img.UserId)
                   .IsRequired();
        }
    }
}
