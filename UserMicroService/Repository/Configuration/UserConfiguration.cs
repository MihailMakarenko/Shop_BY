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

            builder.Property(u => u.RefreshToken).HasMaxLength(500);

            builder.Property(u => u.UserName).HasMaxLength(256).IsRequired().HasComputedColumnSql("[Email]");

            builder.Property(u => u.NormalizedUserName).HasMaxLength(256).IsRequired().HasComputedColumnSql("UPPER([Email])");

            builder.HasData(
            new User
            {
                Id = "a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d",
                UserName = "ivanov@example.com",
                NormalizedUserName = "IVANOV@EXAMPLE.COM",
                Email = "ivanov@example.com",
                NormalizedEmail = "IVANOV@EXAMPLE.COM",
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "+79161234567",
                PasswordHash = "AQAAAAIAAYagAAAAENYUiMMwEz6mK7Q9UngGzqZcmbadl27fTu/Vl2sSZxzVO6gg8eiFojVEgpwhWrW5/Q==",
                AccessFailedCount = 0,
                ConcurrencyStamp = "60bdd23a-6978-4d86-bcf0-6daa2f026a59",
                LockoutEnabled = false,
                LockoutEnd = null,
                RefreshToken = null,
                RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                SecurityStamp = "5b442d41-f7b2-4d1b-a4cb-45d79822923f",
                TwoFactorEnabled = false
            },
            new User
            {
                Id = "b5c6d7e8-f9a0-4b1c-8d2e-3f4a5b6c7d8e",
                UserName = "petrov@example.com",
                NormalizedUserName = "PETROV@EXAMPLE.COM",
                Email = "petrov@example.com",
                NormalizedEmail = "PETROV@EXAMPLE.COM",
                FirstName = "Петр",
                LastName = "Петров",
                PhoneNumber = "+79162345678",
                PasswordHash = "AQAAAAIAAYagAAAAEJ+QzS07ERg3xayKLU6rkLlNVDviOGgbhjRIyC/z+ymKf9iZDbcONU6W+auKqmW8/A==",
                AccessFailedCount = 0,
                ConcurrencyStamp = "70bdd23a-6978-4d86-bcf0-6daa2f026a59",
                LockoutEnabled = false,
                LockoutEnd = null,
                RefreshToken = null,
                RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                SecurityStamp = "6b442d41-f7b2-4d1b-a4cb-45d79822923f",
                TwoFactorEnabled = false
            },
            new User
            {
                Id = "c9d8e7f6-5a4b-3c2d-1e0f-9a8b7c6d5e4f",
                UserName = "sergeev@example.com",
                NormalizedUserName = "SERGEEV@EXAMPLE.COM",
                Email = "sergeev@example.com",
                NormalizedEmail = "SERGEEV@EXAMPLE.COM",
                FirstName = "Сергей",
                LastName = "Сергеев",
                PhoneNumber = "+79163456789",
                PasswordHash = "AQAAAAIAAYagAAAAEBqikCOKD76Hxo7mmGNse297oJ8qvLFj1gCsI4AiiB6FyqVDWYOfKepCBaX1/4deSw==",
                AccessFailedCount = 0,
                ConcurrencyStamp = "80bdd23a-6978-4d86-bcf0-6daa2f026a59",
                LockoutEnabled = false,
                LockoutEnd = null,
                RefreshToken = null,
                RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                SecurityStamp = "7b442d41-f7b2-4d1b-a4cb-45d79822923f",
                TwoFactorEnabled = false
            },
            new User
            {
                Id = "d4e5f6a7-b8c9-4d0e-1f2a-3b4c5d6e7f8a",
                UserName = "andreeva@example.com",
                NormalizedUserName = "ANDREEVA@EXAMPLE.COM",
                Email = "andreeva@example.com",
                NormalizedEmail = "ANDREEVA@EXAMPLE.COM",
                FirstName = "Анна",
                LastName = "Андреева",
                PhoneNumber = "+79164567890",
                PasswordHash = "AQAAAAIAAYagAAAAEBZu/3FjMApsOJ+8UfgKD2Mg1gi7e++9FrG0sMm+PQDh9iI7H21OtfB4vn4T0/EKzA==",
                AccessFailedCount = 0,
                ConcurrencyStamp = "90bdd23a-6978-4d86-bcf0-6daa2f026a59",
                LockoutEnabled = false,
                LockoutEnd = null,
                RefreshToken = null,
                RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                SecurityStamp = "8b442d41-f7b2-4d1b-a4cb-45d79822923f",
                TwoFactorEnabled = false
            },
            new User
            {
                Id = "e3f4a5b6-c7d8-4e9f-0a1b-2c3d4e5f6a7b",
                UserName = "mihailova@example.com",
                NormalizedUserName = "MIHAILOVA@EXAMPLE.COM",
                Email = "mihailova@example.com",
                NormalizedEmail = "MIHAILOVA@EXAMPLE.COM",
                FirstName = "Мария",
                LastName = "Михайлова",
                PhoneNumber = "+79165678901",
                PasswordHash = "AQAAAAIAAYagAAAAEIpil4qB2u3lrsM9lWAhe69Fyl6oB3JVILqtlbmCDrRl6slMntV7C1w8A9JOp73uFg==",
                AccessFailedCount = 0,
                ConcurrencyStamp = "10bdd23a-6978-4d86-bcf0-6daa2f026a59",
                LockoutEnabled = false,
                LockoutEnd = null,
                RefreshToken = null,
                RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                SecurityStamp = "9b442d41-f7b2-4d1b-a4cb-45d79822923f",
                TwoFactorEnabled = false
            },
            new User
            {
                Id = "f2a3b4c5-d6e7-4f8a-9b0c-1d2e3f4a5b6c",
                UserName = "alekseev@example.com",
                NormalizedUserName = "ALEKSEEV@EXAMPLE.COM",
                Email = "alekseev@example.com",
                NormalizedEmail = "ALEKSEEV@EXAMPLE.COM",
                FirstName = "Алексей",
                LastName = "Алексеев",
                PhoneNumber = "+79166789012",
                PasswordHash = "AQAAAAIAAYagAAAAEOTu1amo3Go+posodIMBkdR1AfnYnFEiCru/z0WP008MVwp6UvZJPQf0UsvMCDhbVg==",
                AccessFailedCount = 0,
                ConcurrencyStamp = "20bdd23a-6978-4d86-bcf0-6daa2f026a59",
                LockoutEnabled = false,
                LockoutEnd = null,
                RefreshToken = null,
                RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                SecurityStamp = "0c442d41-f7b2-4d1b-a4cb-45d79822923f",
                TwoFactorEnabled = false
            },
            new User
            {
                Id = "1a2b3c4d-5e6f-4a7b-8c9d-0e1f2a3b4c5d",
                UserName = "olegova@example.com",
                NormalizedUserName = "OLEGOVA@EXAMPLE.COM",
                Email = "olegova@example.com",
                NormalizedEmail = "OLEGOVA@EXAMPLE.COM",
                FirstName = "Ольга",
                LastName = "Олегова",
                PhoneNumber = "+79167890123",
                PasswordHash = "AQAAAAIAAYagAAAAENYUiMMwEz6mK7Q9UngGzqZcmbadl27fTu/Vl2sSZxzVO6gg8eiFojVEgpwhWrW5/Q==",
                AccessFailedCount = 0,
                ConcurrencyStamp = "30bdd23a-6978-4d86-bcf0-6daa2f026a59",
                LockoutEnabled = false,
                LockoutEnd = null,
                RefreshToken = null,
                RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                SecurityStamp = "1c442d41-f7b2-4d1b-a4cb-45d79822923f",
                TwoFactorEnabled = false
            },
            new User
            {
                Id = "2b3c4d5e-6f7a-4b8c-9d0e-1f2a3b4c5d6e",
                UserName = "dmitriev@example.com",
                NormalizedUserName = "DMITRIEV@EXAMPLE.COM",
                Email = "dmitriev@example.com",
                NormalizedEmail = "DMITRIEV@EXAMPLE.COM",
                FirstName = "Дмитрий",
                LastName = "Дмитриев",
                PhoneNumber = "+79168901234",
                PasswordHash = "AQAAAAIAAYagAAAAEJ+QzS07ERg3xayKLU6rkLlNVDviOGgbhjRIyC/z+ymKf9iZDbcONU6W+auKqmW8/A==",
                AccessFailedCount = 0,
                ConcurrencyStamp = "40bdd23a-6978-4d86-bcf0-6daa2f026a59",
                LockoutEnabled = false,
                LockoutEnd = null,
                RefreshToken = null,
                RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                SecurityStamp = "2c442d41-f7b2-4d1b-a4cb-45d79822923f",
                TwoFactorEnabled = false
            },
            new User
            {
                Id = "3c4d5e6f-7a8b-4c9d-0e1f-2a3b4c5d6e7f",
                UserName = "evgenevna@example.com",
                NormalizedUserName = "EVGENEVNA@EXAMPLE.COM",
                Email = "evgenevna@example.com",
                NormalizedEmail = "EVGENEVNA@EXAMPLE.COM",
                FirstName = "Елена",
                LastName = "Евгеньева",
                PhoneNumber = "+79169012345",
                PasswordHash = "AQAAAAIAAYagAAAAEBqikCOKD76Hxo7mmGNse297oJ8qvLFj1gCsI4AiiB6FyqVDWYOfKepCBaX1/4deSw==",
                AccessFailedCount = 0,
                ConcurrencyStamp = "50bdd23a-6978-4d86-bcf0-6daa2f026a59",
                LockoutEnabled = false,
                LockoutEnd = null,
                RefreshToken = null,
                RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                SecurityStamp = "3c442d41-f7b2-4d1b-a4cb-45d79822923f",
                TwoFactorEnabled = false
            },
            new User
            {
                Id = "4d5e6f7a-8b9c-4d0e-1f2a-3b4c5d6e7f8a",
                UserName = "nikolaev@example.com",
                NormalizedUserName = "NIKOLAEV@EXAMPLE.COM",
                Email = "nikolaev@example.com",
                NormalizedEmail = "NIKOLAEV@EXAMPLE.COM",
                FirstName = "Николай",
                LastName = "Николаев",
                PhoneNumber = "+79160123456",
                PasswordHash = "AQAAAAIAAYagAAAAECd8g4VY+JRSDLpo4qHrBTqylzzbNEqISXuZM8tfI9PIlgSYASY2AEcTdinQQCj64w==",
                AccessFailedCount = 0,
                ConcurrencyStamp = "60bdd23a-6978-4d86-bcf0-6daa2f026a59",
                LockoutEnabled = false,
                LockoutEnd = null,
                RefreshToken = null,
                RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                SecurityStamp = "4c442d41-f7b2-4d1b-a4cb-45d79822923f",
                TwoFactorEnabled = false
            }
  );

        }
    }
}
