using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ChatAppApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ChatRooms",
                newName: "RoomName");

            migrationBuilder.RenameColumn(
                name: "UserDisplayName",
                table: "ChatMessages",
                newName: "User");

            migrationBuilder.RenameColumn(
                name: "MessageId",
                table: "ChatMessages",
                newName: "ChatId");

            migrationBuilder.InsertData(
                table: "ChatRooms",
                columns: new[] { "RoomId", "RoomName" },
                values: new object[,]
                {
                    { 1, "General" },
                    { 2, "Test 1" }
                });

            migrationBuilder.InsertData(
                table: "ChatMessages",
                columns: new[] { "ChatId", "MessageBody", "RoomId", "Timestamp", "User" },
                values: new object[,]
                {
                    { 1, "Hey guys", 1, new DateTime(2024, 8, 24, 20, 34, 9, 494, DateTimeKind.Utc).AddTicks(377), "Alice" },
                    { 2, "Any1 on?", 1, new DateTime(2024, 8, 24, 20, 34, 9, 494, DateTimeKind.Utc).AddTicks(378), "Alice" },
                    { 3, "TESSSST", 2, new DateTime(2024, 8, 24, 20, 34, 9, 494, DateTimeKind.Utc).AddTicks(379), "Bob" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ChatRooms",
                keyColumn: "RoomId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ChatRooms",
                keyColumn: "RoomId",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "RoomName",
                table: "ChatRooms",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "User",
                table: "ChatMessages",
                newName: "UserDisplayName");

            migrationBuilder.RenameColumn(
                name: "ChatId",
                table: "ChatMessages",
                newName: "MessageId");
        }
    }
}
