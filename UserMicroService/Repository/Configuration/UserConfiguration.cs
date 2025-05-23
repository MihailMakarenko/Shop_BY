using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Email).IsRequired().HasMaxLength(256);

            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.NormalizedEmail).IsRequired().HasMaxLength(256).HasComputedColumnSql("UPPER([Email])");

            builder.HasIndex(u => u.NormalizedEmail).IsUnique();

            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(100);

            builder.Property(u => u.LastName).IsRequired().HasMaxLength(100);

            builder.Property(u => u.PhoneNumber).HasMaxLength(20);

            builder.HasIndex(u => u.PhoneNumber).IsUnique();

            builder.Property(u => u.RefreshToken).HasMaxLength(500);



        }
    }
}
