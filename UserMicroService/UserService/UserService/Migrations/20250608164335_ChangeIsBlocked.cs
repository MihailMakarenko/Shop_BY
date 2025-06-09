using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIsBlocked : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "isBlocked",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1a2b3c4d-5e6f-4a7b-8c9d-0e1f2a3b4c5d",
                column: "isBlocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2b3c4d5e-6f7a-4b8c-9d0e-1f2a3b4c5d6e",
                column: "isBlocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3c4d5e6f-7a8b-4c9d-0e1f-2a3b4c5d6e7f",
                column: "isBlocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d5e6f7a-8b9c-4d0e-1f2a-3b4c5d6e7f8a",
                column: "isBlocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d",
                column: "isBlocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b5c6d7e8-f9a0-4b1c-8d2e-3f4a5b6c7d8e",
                column: "isBlocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c9d8e7f6-5a4b-3c2d-1e0f-9a8b7c6d5e4f",
                column: "isBlocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d4e5f6a7-b8c9-4d0e-1f2a-3b4c5d6e7f8a",
                column: "isBlocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e3f4a5b6-c7d8-4e9f-0a1b-2c3d4e5f6a7b",
                column: "isBlocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f2a3b4c5-d6e7-4f8a-9b0c-1d2e3f4a5b6c",
                column: "isBlocked",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "isBlocked",
                table: "AspNetUsers",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1a2b3c4d-5e6f-4a7b-8c9d-0e1f2a3b4c5d",
                column: "isBlocked",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2b3c4d5e-6f7a-4b8c-9d0e-1f2a3b4c5d6e",
                column: "isBlocked",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3c4d5e6f-7a8b-4c9d-0e1f-2a3b4c5d6e7f",
                column: "isBlocked",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d5e6f7a-8b9c-4d0e-1f2a-3b4c5d6e7f8a",
                column: "isBlocked",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d",
                column: "isBlocked",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b5c6d7e8-f9a0-4b1c-8d2e-3f4a5b6c7d8e",
                column: "isBlocked",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c9d8e7f6-5a4b-3c2d-1e0f-9a8b7c6d5e4f",
                column: "isBlocked",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d4e5f6a7-b8c9-4d0e-1f2a-3b4c5d6e7f8a",
                column: "isBlocked",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e3f4a5b6-c7d8-4e9f-0a1b-2c3d4e5f6a7b",
                column: "isBlocked",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f2a3b4c5-d6e7-4f8a-9b0c-1d2e3f4a5b6c",
                column: "isBlocked",
                value: null);
        }
    }
}
