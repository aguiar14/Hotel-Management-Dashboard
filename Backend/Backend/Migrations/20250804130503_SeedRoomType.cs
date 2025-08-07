using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoomType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "A room for one person.", "Single", new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "A room for two persons.", "Double", new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "A luxurious suite with additional amenities.", "Suite", new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
