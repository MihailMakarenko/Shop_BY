using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);

            builder.Property(p => p.Description).IsRequired().HasMaxLength(1000);

            builder.Property(p => p.Price).HasColumnType("decimal(18,2)").HasPrecision(18, 2);

            builder.Property(p => p.IsAvailable).HasDefaultValue(true);

            builder.Property(p => p.CreatedByUserId).IsRequired();

            builder.Property(p => p.CreatedAt).HasDefaultValueSql("GETUTCDATE()").ValueGeneratedOnAdd();

            builder.Property(p => p.UpdateAt).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("GETUTCDATE()");

            builder.Property(p => p.CreatedByUserId).IsRequired();

            builder.HasIndex(p => p.CreatedByUserId);


        }

     

        //public ProductConfiguration(EntityTypeBuilder<Product> builder)
        //{


        //}
    }
}
