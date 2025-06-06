using Entities.Models;
using Microsoft.Extensions.DependencyInjection;
using Repository;

namespace UserService.Tests.IntegrationTests.Helpers
{
    public static class SeedData
    {
        public static void ResetData()
        {
            using var scope = new Factory<Program>().Services.CreateScope();
            using var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            SeedTestData(appContext);
        }

        public static void SeedTestData(AppDbContext appDbContext)
        {
            appDbContext.Users.RemoveRange(appDbContext.Users.ToList());
            appDbContext.SaveChanges();

            appDbContext.Users.AddRange(new List<User>()
            {
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
                PasswordHash = "AQAAAAIAAYagAAAAEIuUFrY17ROBkwTOjw/IpgwyHWRUb9aRl3YvT8wJ/Deo+0m8k71mKCf1mLw+/95cuQ==",
                AccessFailedCount = 0,
                ConcurrencyStamp = "60bdd23a-6978-4d86-bcf0-6daa2f026a59",
                LockoutEnabled = false,
                LockoutEnd = null,
                RefreshToken = null,
                RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                SecurityStamp = "5b442d41-f7b2-4d1b-a4cb-45d79822923f",
                TwoFactorEnabled = false,
                EmailConfirmToken = "11111111-2222-3333-4444-555555555555",
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
            });
            appDbContext.SaveChanges();
        }
    }

}
