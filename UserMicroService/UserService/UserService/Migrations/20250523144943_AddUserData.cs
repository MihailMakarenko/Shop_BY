using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class AddUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1a2b3c4d-5e6f-4a7b-8c9d-0e1f2a3b4c5d", 0, "30bdd23a-6978-4d86-bcf0-6daa2f026a59", "olegova@example.com", false, "Ольга", "Олегова", false, null, "OLEGOVA@EXAMPLE.COM", "AQAAAAIAAYagAAAAENYUiMMwEz6mK7Q9UngGzqZcmbadl27fTu/Vl2sSZxzVO6gg8eiFojVEgpwhWrW5/Q==", "+79167890123", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "1c442d41-f7b2-4d1b-a4cb-45d79822923f", false, "olegova@example.com" },
                    { "2b3c4d5e-6f7a-4b8c-9d0e-1f2a3b4c5d6e", 0, "40bdd23a-6978-4d86-bcf0-6daa2f026a59", "dmitriev@example.com", false, "Дмитрий", "Дмитриев", false, null, "DMITRIEV@EXAMPLE.COM", "AQAAAAIAAYagAAAAEJ+QzS07ERg3xayKLU6rkLlNVDviOGgbhjRIyC/z+ymKf9iZDbcONU6W+auKqmW8/A==", "+79168901234", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "2c442d41-f7b2-4d1b-a4cb-45d79822923f", false, "dmitriev@example.com" },
                    { "3c4d5e6f-7a8b-4c9d-0e1f-2a3b4c5d6e7f", 0, "50bdd23a-6978-4d86-bcf0-6daa2f026a59", "evgenevna@example.com", false, "Елена", "Евгеньева", false, null, "EVGENEVNA@EXAMPLE.COM", "AQAAAAIAAYagAAAAEBqikCOKD76Hxo7mmGNse297oJ8qvLFj1gCsI4AiiB6FyqVDWYOfKepCBaX1/4deSw==", "+79169012345", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "3c442d41-f7b2-4d1b-a4cb-45d79822923f", false, "evgenevna@example.com" },
                    { "4d5e6f7a-8b9c-4d0e-1f2a-3b4c5d6e7f8a", 0, "60bdd23a-6978-4d86-bcf0-6daa2f026a59", "nikolaev@example.com", false, "Николай", "Николаев", false, null, "NIKOLAEV@EXAMPLE.COM", "AQAAAAIAAYagAAAAECd8g4VY+JRSDLpo4qHrBTqylzzbNEqISXuZM8tfI9PIlgSYASY2AEcTdinQQCj64w==", "+79160123456", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "4c442d41-f7b2-4d1b-a4cb-45d79822923f", false, "nikolaev@example.com" },
                    { "a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d", 0, "60bdd23a-6978-4d86-bcf0-6daa2f026a59", "ivanov@example.com", false, "Иван", "Иванов", false, null, "IVANOV@EXAMPLE.COM", "AQAAAAIAAYagAAAAENYUiMMwEz6mK7Q9UngGzqZcmbadl27fTu/Vl2sSZxzVO6gg8eiFojVEgpwhWrW5/Q==", "+79161234567", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "5b442d41-f7b2-4d1b-a4cb-45d79822923f", false, "ivanov@example.com" },
                    { "b5c6d7e8-f9a0-4b1c-8d2e-3f4a5b6c7d8e", 0, "70bdd23a-6978-4d86-bcf0-6daa2f026a59", "petrov@example.com", false, "Петр", "Петров", false, null, "PETROV@EXAMPLE.COM", "AQAAAAIAAYagAAAAEJ+QzS07ERg3xayKLU6rkLlNVDviOGgbhjRIyC/z+ymKf9iZDbcONU6W+auKqmW8/A==", "+79162345678", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "6b442d41-f7b2-4d1b-a4cb-45d79822923f", false, "petrov@example.com" },
                    { "c9d8e7f6-5a4b-3c2d-1e0f-9a8b7c6d5e4f", 0, "80bdd23a-6978-4d86-bcf0-6daa2f026a59", "sergeev@example.com", false, "Сергей", "Сергеев", false, null, "SERGEEV@EXAMPLE.COM", "AQAAAAIAAYagAAAAEBqikCOKD76Hxo7mmGNse297oJ8qvLFj1gCsI4AiiB6FyqVDWYOfKepCBaX1/4deSw==", "+79163456789", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "7b442d41-f7b2-4d1b-a4cb-45d79822923f", false, "sergeev@example.com" },
                    { "d4e5f6a7-b8c9-4d0e-1f2a-3b4c5d6e7f8a", 0, "90bdd23a-6978-4d86-bcf0-6daa2f026a59", "andreeva@example.com", false, "Анна", "Андреева", false, null, "ANDREEVA@EXAMPLE.COM", "AQAAAAIAAYagAAAAEBZu/3FjMApsOJ+8UfgKD2Mg1gi7e++9FrG0sMm+PQDh9iI7H21OtfB4vn4T0/EKzA==", "+79164567890", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "8b442d41-f7b2-4d1b-a4cb-45d79822923f", false, "andreeva@example.com" },
                    { "e3f4a5b6-c7d8-4e9f-0a1b-2c3d4e5f6a7b", 0, "10bdd23a-6978-4d86-bcf0-6daa2f026a59", "mihailova@example.com", false, "Мария", "Михайлова", false, null, "MIHAILOVA@EXAMPLE.COM", "AQAAAAIAAYagAAAAEIpil4qB2u3lrsM9lWAhe69Fyl6oB3JVILqtlbmCDrRl6slMntV7C1w8A9JOp73uFg==", "+79165678901", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "9b442d41-f7b2-4d1b-a4cb-45d79822923f", false, "mihailova@example.com" },
                    { "f2a3b4c5-d6e7-4f8a-9b0c-1d2e3f4a5b6c", 0, "20bdd23a-6978-4d86-bcf0-6daa2f026a59", "alekseev@example.com", false, "Алексей", "Алексеев", false, null, "ALEKSEEV@EXAMPLE.COM", "AQAAAAIAAYagAAAAEOTu1amo3Go+posodIMBkdR1AfnYnFEiCru/z0WP008MVwp6UvZJPQf0UsvMCDhbVg==", "+79166789012", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "0c442d41-f7b2-4d1b-a4cb-45d79822923f", false, "alekseev@example.com" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1a2b3c4d-5e6f-4a7b-8c9d-0e1f2a3b4c5d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2b3c4d5e-6f7a-4b8c-9d0e-1f2a3b4c5d6e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3c4d5e6f-7a8b-4c9d-0e1f-2a3b4c5d6e7f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d5e6f7a-8b9c-4d0e-1f2a-3b4c5d6e7f8a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b5c6d7e8-f9a0-4b1c-8d2e-3f4a5b6c7d8e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c9d8e7f6-5a4b-3c2d-1e0f-9a8b7c6d5e4f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d4e5f6a7-b8c9-4d0e-1f2a-3b4c5d6e7f8a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e3f4a5b6-c7d8-4e9f-0a1b-2c3d4e5f6a7b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f2a3b4c5-d6e7-4f8a-9b0c-1d2e3f4a5b6c");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
