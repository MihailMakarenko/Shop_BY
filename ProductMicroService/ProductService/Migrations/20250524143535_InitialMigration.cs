using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductService.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "CreatedByUserId", "Description", "IsAvailable", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("a7a7a7a7-7777-7777-7777-777777777777"), new DateTime(2023, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "1a2b3c4d-5e6f-4a7b-8c9d-0e1f2a3b4c5d", "Игровая консоль нового поколения", true, "PlayStation 5", 79990m },
                    { new Guid("a8a8a8a8-1111-1111-1111-111111111111"), new DateTime(2023, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d", "Флагманский смартфон с камерой 200 МП", true, "Смартфон Samsung Galaxy S23", 89990m }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "CreatedByUserId", "Description", "Name", "Price" },
                values: new object[] { new Guid("b6b6b6b6-8888-8888-8888-888888888888"), new DateTime(2023, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "2b3c4d5e-6f7a-4b8c-9d0e-1f2a3b4c5d6e", "4K монитор с поддержкой HDR", "Монитор LG UltraFine 32UN880", 89990m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "CreatedByUserId", "Description", "IsAvailable", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("b7b7b7b7-2222-2222-2222-222222222222"), new DateTime(2023, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "b5c6d7e8-f9a0-4b1c-8d2e-3f4a5b6c7d8e", "Игровой ноутбук с RTX 4090", true, "Ноутбук ASUS ROG Zephyrus", 249990m },
                    { new Guid("c5c5c5c5-9999-9999-9999-999999999999"), new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "3c4d5e6f-7a8b-4c9d-0e1f-2a3b4c5d6e7f", "Беспроводная клавиатура с подсветкой", true, "Клавиатура Logitech MX Keys", 12990m },
                    { new Guid("c6c6c6c6-3333-3333-3333-333333333333"), new DateTime(2023, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "c9d8e7f6-5a4b-3c2d-1e0f-9a8b7c6d5e4f", "Беспроводные наушники с шумоподавлением", true, "Наушники Sony WH-1000XM5", 34990m },
                    { new Guid("d4d4d4d4-0000-0000-0000-000000000000"), new DateTime(2023, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "4d5e6f7a-8b9c-4d0e-1f2a-3b4c5d6e7f8a", "Портативный SSD с USB 3.2 Gen 2", true, "SSD Samsung T7 1TB", 10990m }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "CreatedByUserId", "Description", "Name", "Price" },
                values: new object[] { new Guid("d5d5d5d5-4444-4444-4444-444444444444"), new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "d4e5f6a7-b8c9-4d0e-1f2a-3b4c5d6e7f8a", "Смарт-часы с функцией ЭКГ", "Apple Watch Series 9", 45990m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "CreatedByUserId", "Description", "IsAvailable", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("e4e4e4e4-5555-5555-5555-555555555555"), new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "e3f4a5b6-c7d8-4e9f-0a1b-2c3d4e5f6a7b", "Зеркальный фотоаппарат 45 МП", true, "Фотоаппарат Canon EOS R5", 379990m },
                    { new Guid("f3f3f3f3-6666-6666-6666-666666666666"), new DateTime(2023, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "f2a3b4c5-d6e7-4f8a-9b0c-1d2e3f4a5b6c", "Читалка с экраном E Ink Carta", true, "Электронная книга PocketBook 740", 19990m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedByUserId",
                table: "Products",
                column: "CreatedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
