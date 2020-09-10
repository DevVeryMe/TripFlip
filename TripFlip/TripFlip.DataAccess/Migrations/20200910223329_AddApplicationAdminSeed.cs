using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TripFlip.DataAccess.Migrations
{
    public partial class AddApplicationAdminSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7571d22b-4c41-4ade-8c80-beb91a87fd4a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b8dbadc4-a1c3-4012-96c7-cce154962375"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f4053bd9-1fda-40b6-9e53-4e83b2a3feba"));

            migrationBuilder.InsertData(
                table: "ApplicationRoles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Admin" });

            migrationBuilder.UpdateData(
                table: "ItemLists",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 11, 1, 33, 27, 915, DateTimeKind.Unspecified).AddTicks(9956), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ItemLists",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 11, 1, 33, 27, 916, DateTimeKind.Unspecified).AddTicks(1533), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 11, 1, 33, 27, 915, DateTimeKind.Unspecified).AddTicks(4415), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 11, 1, 33, 27, 915, DateTimeKind.Unspecified).AddTicks(5844), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 11, 1, 33, 27, 915, DateTimeKind.Unspecified).AddTicks(5877), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 11, 1, 33, 27, 915, DateTimeKind.Unspecified).AddTicks(5884), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 11, 1, 33, 27, 915, DateTimeKind.Unspecified).AddTicks(5890), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 11, 1, 33, 27, 915, DateTimeKind.Unspecified).AddTicks(5896), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 11, 1, 33, 27, 915, DateTimeKind.Unspecified).AddTicks(5902), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 11, 1, 33, 27, 915, DateTimeKind.Unspecified).AddTicks(5909), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 11, 1, 33, 27, 915, DateTimeKind.Unspecified).AddTicks(5915), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 11, 1, 33, 27, 915, DateTimeKind.Unspecified).AddTicks(5921), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 11, 1, 33, 27, 914, DateTimeKind.Unspecified).AddTicks(7155), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 11, 1, 33, 27, 914, DateTimeKind.Unspecified).AddTicks(8411), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TaskLists",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 11, 1, 33, 27, 916, DateTimeKind.Unspecified).AddTicks(9949), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 11, 1, 33, 27, 917, DateTimeKind.Unspecified).AddTicks(3851), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 11, 1, 33, 27, 917, DateTimeKind.Unspecified).AddTicks(5839), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 11, 1, 33, 27, 917, DateTimeKind.Unspecified).AddTicks(5871), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 11, 1, 33, 27, 917, DateTimeKind.Unspecified).AddTicks(5878), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 11, 1, 33, 27, 917, DateTimeKind.Unspecified).AddTicks(5884), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 11, 1, 33, 27, 907, DateTimeKind.Unspecified).AddTicks(8529), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateCreated", "Email", "PasswordHash" },
                values: new object[,]
                {
                    { new Guid("17e9456a-a362-46ca-9cd4-3dc1cd2061ff"), new DateTimeOffset(new DateTime(2020, 9, 11, 1, 33, 27, 917, DateTimeKind.Unspecified).AddTicks(9910), new TimeSpan(0, 3, 0, 0, 0)), "sample1.email@mail.com", "some_hash" },
                    { new Guid("3a59e4b7-3eea-4494-81eb-c72f8e4e2498"), new DateTimeOffset(new DateTime(2020, 9, 11, 1, 33, 27, 918, DateTimeKind.Unspecified).AddTicks(846), new TimeSpan(0, 3, 0, 0, 0)), "sample2.email@mail.com", "some_other_hash" },
                    { new Guid("f274863c-7ba0-45c4-80d5-e9093b3c5ac6"), new DateTimeOffset(new DateTime(2020, 9, 11, 1, 33, 27, 918, DateTimeKind.Unspecified).AddTicks(1233), new TimeSpan(0, 3, 0, 0, 0)), "sample3.email@mail.com", "hash_hash" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("17e9456a-a362-46ca-9cd4-3dc1cd2061ff"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3a59e4b7-3eea-4494-81eb-c72f8e4e2498"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f274863c-7ba0-45c4-80d5-e9093b3c5ac6"));

            migrationBuilder.UpdateData(
                table: "ItemLists",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 7, 21, 43, 55, 210, DateTimeKind.Unspecified).AddTicks(3), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ItemLists",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 7, 21, 43, 55, 210, DateTimeKind.Unspecified).AddTicks(547), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 7, 21, 43, 55, 209, DateTimeKind.Unspecified).AddTicks(7516), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 7, 21, 43, 55, 209, DateTimeKind.Unspecified).AddTicks(8095), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 7, 21, 43, 55, 209, DateTimeKind.Unspecified).AddTicks(8124), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 7, 21, 43, 55, 209, DateTimeKind.Unspecified).AddTicks(8131), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 7, 21, 43, 55, 209, DateTimeKind.Unspecified).AddTicks(8137), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 7, 21, 43, 55, 209, DateTimeKind.Unspecified).AddTicks(8143), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 7, 21, 43, 55, 209, DateTimeKind.Unspecified).AddTicks(8149), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 7, 21, 43, 55, 209, DateTimeKind.Unspecified).AddTicks(8154), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 7, 21, 43, 55, 209, DateTimeKind.Unspecified).AddTicks(8160), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 7, 21, 43, 55, 209, DateTimeKind.Unspecified).AddTicks(8166), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 7, 21, 43, 55, 209, DateTimeKind.Unspecified).AddTicks(4053), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 7, 21, 43, 55, 209, DateTimeKind.Unspecified).AddTicks(4811), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TaskLists",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 7, 21, 43, 55, 210, DateTimeKind.Unspecified).AddTicks(4704), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 7, 21, 43, 55, 210, DateTimeKind.Unspecified).AddTicks(6662), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 7, 21, 43, 55, 210, DateTimeKind.Unspecified).AddTicks(7615), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 7, 21, 43, 55, 210, DateTimeKind.Unspecified).AddTicks(7650), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 7, 21, 43, 55, 210, DateTimeKind.Unspecified).AddTicks(7657), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 7, 21, 43, 55, 210, DateTimeKind.Unspecified).AddTicks(7663), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 7, 21, 43, 55, 206, DateTimeKind.Unspecified).AddTicks(217), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateCreated", "Email", "PasswordHash" },
                values: new object[,]
                {
                    { new Guid("f4053bd9-1fda-40b6-9e53-4e83b2a3feba"), new DateTimeOffset(new DateTime(2020, 9, 7, 21, 43, 55, 211, DateTimeKind.Unspecified).AddTicks(157), new TimeSpan(0, 3, 0, 0, 0)), "sample3.email@mail.com", "hash_hash" },
                    { new Guid("b8dbadc4-a1c3-4012-96c7-cce154962375"), new DateTimeOffset(new DateTime(2020, 9, 7, 21, 43, 55, 211, DateTimeKind.Unspecified).AddTicks(127), new TimeSpan(0, 3, 0, 0, 0)), "sample2.email@mail.com", "some_other_hash" },
                    { new Guid("7571d22b-4c41-4ade-8c80-beb91a87fd4a"), new DateTimeOffset(new DateTime(2020, 9, 7, 21, 43, 55, 210, DateTimeKind.Unspecified).AddTicks(9599), new TimeSpan(0, 3, 0, 0, 0)), "sample1.email@mail.com", "some_hash" }
                });
        }
    }
}
