using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PhoneNumber",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                computedColumnSql: "[Email]",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedUserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                computedColumnSql: "UPPER([Email])",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "User", "USER" },
                    { "2", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldComputedColumnSql: "[Email]");

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedUserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldComputedColumnSql: "UPPER([Email])");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1a2b3c4d-5e6f-4a7b-8c9d-0e1f2a3b4c5d",
                columns: new[] { "NormalizedUserName", "UserName" },
                values: new object[] { "OLEGOVA@EXAMPLE.COM", "olegova@example.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2b3c4d5e-6f7a-4b8c-9d0e-1f2a3b4c5d6e",
                columns: new[] { "NormalizedUserName", "UserName" },
                values: new object[] { "DMITRIEV@EXAMPLE.COM", "dmitriev@example.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3c4d5e6f-7a8b-4c9d-0e1f-2a3b4c5d6e7f",
                columns: new[] { "NormalizedUserName", "UserName" },
                values: new object[] { "EVGENEVNA@EXAMPLE.COM", "evgenevna@example.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d5e6f7a-8b9c-4d0e-1f2a-3b4c5d6e7f8a",
                columns: new[] { "NormalizedUserName", "UserName" },
                values: new object[] { "NIKOLAEV@EXAMPLE.COM", "nikolaev@example.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d",
                columns: new[] { "NormalizedUserName", "UserName" },
                values: new object[] { "IVANOV@EXAMPLE.COM", "ivanov@example.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b5c6d7e8-f9a0-4b1c-8d2e-3f4a5b6c7d8e",
                columns: new[] { "NormalizedUserName", "UserName" },
                values: new object[] { "PETROV@EXAMPLE.COM", "petrov@example.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c9d8e7f6-5a4b-3c2d-1e0f-9a8b7c6d5e4f",
                columns: new[] { "NormalizedUserName", "UserName" },
                values: new object[] { "SERGEEV@EXAMPLE.COM", "sergeev@example.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d4e5f6a7-b8c9-4d0e-1f2a-3b4c5d6e7f8a",
                columns: new[] { "NormalizedUserName", "UserName" },
                values: new object[] { "ANDREEVA@EXAMPLE.COM", "andreeva@example.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e3f4a5b6-c7d8-4e9f-0a1b-2c3d4e5f6a7b",
                columns: new[] { "NormalizedUserName", "UserName" },
                values: new object[] { "MIHAILOVA@EXAMPLE.COM", "mihailova@example.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f2a3b4c5-d6e7-4f8a-9b0c-1d2e3f4a5b6c",
                columns: new[] { "NormalizedUserName", "UserName" },
                values: new object[] { "ALEKSEEV@EXAMPLE.COM", "alekseev@example.com" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PhoneNumber",
                table: "AspNetUsers",
                column: "PhoneNumber",
                unique: true,
                filter: "[PhoneNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }
    }
}
