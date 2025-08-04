using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class RoomsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Capacity", "CreatedAt", "Description", "IsAvailable", "Number", "PricePerNight", "RoomTypeId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "A cozy single room with a comfortable bed.", true, "101", 100.00m, 1, new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "A spacious double room with two beds.", true, "102", 150.00m, 2, new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 2, new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "A luxurious suite with a king-size bed and a view.", true, "103", 300.00m, 3, new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
