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


            builder.HasData(
            new Product
            {
                Id = new Guid("a8a8a8a8-1111-1111-1111-111111111111"),
                Name = "Смартфон Samsung Galaxy S23",
                Description = "Флагманский смартфон с камерой 200 МП",
                Price = 89990,
                IsAvailable = true,
                CreatedByUserId = "a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d", // Иванов
                CreatedAt = new DateTime(2023, 10, 15),
                UpdateAt = null
            },
            new Product
            {
                Id = new Guid("b7b7b7b7-2222-2222-2222-222222222222"),
                Name = "Ноутбук ASUS ROG Zephyrus",
                Description = "Игровой ноутбук с RTX 4090",
                Price = 249990,
                IsAvailable = true,
                CreatedByUserId = "b5c6d7e8-f9a0-4b1c-8d2e-3f4a5b6c7d8e", // Петров
                CreatedAt = new DateTime(2023, 11, 5),
                UpdateAt = new DateTime(2023, 12, 10)
            },
            new Product
            {
                Id = new Guid("c6c6c6c6-3333-3333-3333-333333333333"),
                Name = "Наушники Sony WH-1000XM5",
                Description = "Беспроводные наушники с шумоподавлением",
                Price = 34990,
                IsAvailable = true,
                CreatedByUserId = "c9d8e7f6-5a4b-3c2d-1e0f-9a8b7c6d5e4f", // Сергеев
                CreatedAt = new DateTime(2023, 9, 20),
                UpdateAt = null
            },
            new Product
            {
                Id = new Guid("d5d5d5d5-4444-4444-4444-444444444444"),
                Name = "Apple Watch Series 9",
                Description = "Смарт-часы с функцией ЭКГ",
                Price = 45990,
                IsAvailable = false,
                CreatedByUserId = "d4e5f6a7-b8c9-4d0e-1f2a-3b4c5d6e7f8a", // Андреева
                CreatedAt = new DateTime(2023, 12, 1),
                UpdateAt = new DateTime(2024, 1, 15)
            },
            new Product
            {
                Id = new Guid("e4e4e4e4-5555-5555-5555-555555555555"),
                Name = "Фотоаппарат Canon EOS R5",
                Description = "Зеркальный фотоаппарат 45 МП",
                Price = 379990,
                IsAvailable = true,
                CreatedByUserId = "e3f4a5b6-c7d8-4e9f-0a1b-2c3d4e5f6a7b", // Михайлова
                CreatedAt = new DateTime(2023, 8, 10),
                UpdateAt = null
            },
            new Product
            {
                Id = new Guid("f3f3f3f3-6666-6666-6666-666666666666"),
                Name = "Электронная книга PocketBook 740",
                Description = "Читалка с экраном E Ink Carta",
                Price = 19990,
                IsAvailable = true,
                CreatedByUserId = "f2a3b4c5-d6e7-4f8a-9b0c-1d2e3f4a5b6c", // Алексеев
                CreatedAt = new DateTime(2023, 7, 25),
                UpdateAt = new DateTime(2023, 9, 5)
            },
            new Product
            {
                Id = new Guid("a7a7a7a7-7777-7777-7777-777777777777"),
                Name = "PlayStation 5",
                Description = "Игровая консоль нового поколения",
                Price = 79990,
                IsAvailable = true,
                CreatedByUserId = "1a2b3c4d-5e6f-4a7b-8c9d-0e1f2a3b4c5d", // Олегова
                CreatedAt = new DateTime(2023, 11, 20),
                UpdateAt = null
            },
            new Product
            {
                Id = new Guid("b6b6b6b6-8888-8888-8888-888888888888"),
                Name = "Монитор LG UltraFine 32UN880",
                Description = "4K монитор с поддержкой HDR",
                Price = 89990,
                IsAvailable = false,
                CreatedByUserId = "2b3c4d5e-6f7a-4b8c-9d0e-1f2a3b4c5d6e", // Дмитриев
                CreatedAt = new DateTime(2023, 10, 5),
                UpdateAt = new DateTime(2023, 11, 15)
            },
            new Product
            {
                Id = new Guid("c5c5c5c5-9999-9999-9999-999999999999"),
                Name = "Клавиатура Logitech MX Keys",
                Description = "Беспроводная клавиатура с подсветкой",
                Price = 12990,
                IsAvailable = true,
                CreatedByUserId = "3c4d5e6f-7a8b-4c9d-0e1f-2a3b4c5d6e7f", // Евгеньева
                CreatedAt = new DateTime(2023, 9, 15),
                UpdateAt = null
            },
            new Product
            {
                Id = new Guid("d4d4d4d4-0000-0000-0000-000000000000"),
                Name = "SSD Samsung T7 1TB",
                Description = "Портативный SSD с USB 3.2 Gen 2",
                Price = 10990,
                IsAvailable = true,
                CreatedByUserId = "4d5e6f7a-8b9c-4d0e-1f2a-3b4c5d6e7f8a", // Николаев
                CreatedAt = new DateTime(2023, 12, 20),
                UpdateAt = null
            }
            );
        }



        //public ProductConfiguration(EntityTypeBuilder<Product> builder)
        //{


        //}
    }
}
