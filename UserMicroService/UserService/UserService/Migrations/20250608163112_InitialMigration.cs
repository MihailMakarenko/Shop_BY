using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmailConfirmToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isBlocked = table.Column<bool>(type: "bit", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, computedColumnSql: "[Email]"),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, computedColumnSql: "UPPER([Email])"),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, computedColumnSql: "UPPER([Email])"),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "User", "USER" },
                    { "2", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "ConcurrencyStamp", "Email", "EmailConfirmToken", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "PasswordHash", "PhoneNumber", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "isBlocked" },
                values: new object[,]
                {
                    { "1a2b3c4d-5e6f-4a7b-8c9d-0e1f2a3b4c5d", "30bdd23a-6978-4d86-bcf0-6daa2f026a59", "olegova@example.com", null, false, "Ольга", "Олегова", false, "AQAAAAIAAYagAAAAENYUiMMwEz6mK7Q9UngGzqZcmbadl27fTu/Vl2sSZxzVO6gg8eiFojVEgpwhWrW5/Q==", "+79167890123", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1c442d41-f7b2-4d1b-a4cb-45d79822923f", null },
                    { "2b3c4d5e-6f7a-4b8c-9d0e-1f2a3b4c5d6e", "40bdd23a-6978-4d86-bcf0-6daa2f026a59", "dmitriev@example.com", null, false, "Дмитрий", "Дмитриев", false, "AQAAAAIAAYagAAAAEJ+QzS07ERg3xayKLU6rkLlNVDviOGgbhjRIyC/z+ymKf9iZDbcONU6W+auKqmW8/A==", "+79168901234", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2c442d41-f7b2-4d1b-a4cb-45d79822923f", null },
                    { "3c4d5e6f-7a8b-4c9d-0e1f-2a3b4c5d6e7f", "50bdd23a-6978-4d86-bcf0-6daa2f026a59", "evgenevna@example.com", null, false, "Елена", "Евгеньева", false, "AQAAAAIAAYagAAAAEBqikCOKD76Hxo7mmGNse297oJ8qvLFj1gCsI4AiiB6FyqVDWYOfKepCBaX1/4deSw==", "+79169012345", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "3c442d41-f7b2-4d1b-a4cb-45d79822923f", null },
                    { "4d5e6f7a-8b9c-4d0e-1f2a-3b4c5d6e7f8a", "60bdd23a-6978-4d86-bcf0-6daa2f026a59", "nikolaev@example.com", null, false, "Николай", "Николаев", false, "AQAAAAIAAYagAAAAECd8g4VY+JRSDLpo4qHrBTqylzzbNEqISXuZM8tfI9PIlgSYASY2AEcTdinQQCj64w==", "+79160123456", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "4c442d41-f7b2-4d1b-a4cb-45d79822923f", null },
                    { "a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d", "60bdd23a-6978-4d86-bcf0-6daa2f026a59", "ivanov@example.com", null, false, "Иван", "Иванов", false, "AQAAAAIAAYagAAAAENYUiMMwEz6mK7Q9UngGzqZcmbadl27fTu/Vl2sSZxzVO6gg8eiFojVEgpwhWrW5/Q==", "+79161234567", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "5b442d41-f7b2-4d1b-a4cb-45d79822923f", null },
                    { "b5c6d7e8-f9a0-4b1c-8d2e-3f4a5b6c7d8e", "70bdd23a-6978-4d86-bcf0-6daa2f026a59", "petrov@example.com", null, false, "Петр", "Петров", false, "AQAAAAIAAYagAAAAEJ+QzS07ERg3xayKLU6rkLlNVDviOGgbhjRIyC/z+ymKf9iZDbcONU6W+auKqmW8/A==", "+79162345678", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "6b442d41-f7b2-4d1b-a4cb-45d79822923f", null },
                    { "c9d8e7f6-5a4b-3c2d-1e0f-9a8b7c6d5e4f", "80bdd23a-6978-4d86-bcf0-6daa2f026a59", "sergeev@example.com", null, false, "Сергей", "Сергеев", false, "AQAAAAIAAYagAAAAEBqikCOKD76Hxo7mmGNse297oJ8qvLFj1gCsI4AiiB6FyqVDWYOfKepCBaX1/4deSw==", "+79163456789", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "7b442d41-f7b2-4d1b-a4cb-45d79822923f", null },
                    { "d4e5f6a7-b8c9-4d0e-1f2a-3b4c5d6e7f8a", "90bdd23a-6978-4d86-bcf0-6daa2f026a59", "andreeva@example.com", null, false, "Анна", "Андреева", false, "AQAAAAIAAYagAAAAEBZu/3FjMApsOJ+8UfgKD2Mg1gi7e++9FrG0sMm+PQDh9iI7H21OtfB4vn4T0/EKzA==", "+79164567890", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "8b442d41-f7b2-4d1b-a4cb-45d79822923f", null },
                    { "e3f4a5b6-c7d8-4e9f-0a1b-2c3d4e5f6a7b", "10bdd23a-6978-4d86-bcf0-6daa2f026a59", "mihailova@example.com", null, false, "Мария", "Михайлова", false, "AQAAAAIAAYagAAAAEIpil4qB2u3lrsM9lWAhe69Fyl6oB3JVILqtlbmCDrRl6slMntV7C1w8A9JOp73uFg==", "+79165678901", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "9b442d41-f7b2-4d1b-a4cb-45d79822923f", null },
                    { "f2a3b4c5-d6e7-4f8a-9b0c-1d2e3f4a5b6c", "20bdd23a-6978-4d86-bcf0-6daa2f026a59", "alekseev@example.com", null, false, "Алексей", "Алексеев", false, "AQAAAAIAAYagAAAAEOTu1amo3Go+posodIMBkdR1AfnYnFEiCru/z0WP008MVwp6UvZJPQf0UsvMCDhbVg==", "+79166789012", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0c442d41-f7b2-4d1b-a4cb-45d79822923f", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
