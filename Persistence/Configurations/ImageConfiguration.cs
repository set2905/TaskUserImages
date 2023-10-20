using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id).HasConversion(id => id.Value,
                                                          val => new(val));
            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(img=>img.UserId);

            builder.HasIndex(i => i.FileName)
                   .IsUnique();
        }
    }
}
