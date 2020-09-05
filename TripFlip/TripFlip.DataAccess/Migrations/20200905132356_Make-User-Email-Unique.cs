using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TripFlip.DataAccess.Migrations
{
    public partial class MakeUserEmailUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11f47509-040d-40d5-9e69-7eab96f00413"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("270faae4-8052-4d37-afa1-235b966b9152"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("afa6ea45-c868-401d-b853-e10912ce68c9"));

            migrationBuilder.UpdateData(
                table: "ItemLists",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 16, 23, 54, 691, DateTimeKind.Unspecified).AddTicks(4396), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ItemLists",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 16, 23, 54, 691, DateTimeKind.Unspecified).AddTicks(4396), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 16, 23, 54, 691, DateTimeKind.Unspecified).AddTicks(4396), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 16, 23, 54, 691, DateTimeKind.Unspecified).AddTicks(4396), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 16, 23, 54, 691, DateTimeKind.Unspecified).AddTicks(4396), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 16, 23, 54, 691, DateTimeKind.Unspecified).AddTicks(4396), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 16, 23, 54, 691, DateTimeKind.Unspecified).AddTicks(4396), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 16, 23, 54, 691, DateTimeKind.Unspecified).AddTicks(4396), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 16, 23, 54, 691, DateTimeKind.Unspecified).AddTicks(4396), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 16, 23, 54, 691, DateTimeKind.Unspecified).AddTicks(4396), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 16, 23, 54, 691, DateTimeKind.Unspecified).AddTicks(4396), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 16, 23, 54, 691, DateTimeKind.Unspecified).AddTicks(4396), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 16, 23, 54, 690, DateTimeKind.Unspecified).AddTicks(4396), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 16, 23, 54, 690, DateTimeKind.Unspecified).AddTicks(4396), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TaskLists",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 16, 23, 54, 692, DateTimeKind.Unspecified).AddTicks(4397), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 16, 23, 54, 692, DateTimeKind.Unspecified).AddTicks(4397), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 16, 23, 54, 692, DateTimeKind.Unspecified).AddTicks(4397), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 16, 23, 54, 692, DateTimeKind.Unspecified).AddTicks(4397), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 16, 23, 54, 692, DateTimeKind.Unspecified).AddTicks(4397), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 16, 23, 54, 692, DateTimeKind.Unspecified).AddTicks(4397), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 16, 23, 54, 684, DateTimeKind.Unspecified).AddTicks(4392), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateCreated", "Email", "PasswordHash" },
                values: new object[,]
                {
                    { new Guid("743ad20d-d566-4761-8c1c-4167ad7a290e"), new DateTimeOffset(new DateTime(2020, 9, 5, 16, 23, 54, 693, DateTimeKind.Unspecified).AddTicks(4397), new TimeSpan(0, 3, 0, 0, 0)), "sample3.email@mail.com", "hash_hash" },
                    { new Guid("4c767643-f0eb-4c27-a646-bdffd34619bb"), new DateTimeOffset(new DateTime(2020, 9, 5, 16, 23, 54, 693, DateTimeKind.Unspecified).AddTicks(4397), new TimeSpan(0, 3, 0, 0, 0)), "sample2.email@mail.com", "some_other_hash" },
                    { new Guid("21ccc963-025b-46d5-bae0-421ea1258a69"), new DateTimeOffset(new DateTime(2020, 9, 5, 16, 23, 54, 692, DateTimeKind.Unspecified).AddTicks(4397), new TimeSpan(0, 3, 0, 0, 0)), "sample1.email@mail.com", "some_hash" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("21ccc963-025b-46d5-bae0-421ea1258a69"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4c767643-f0eb-4c27-a646-bdffd34619bb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("743ad20d-d566-4761-8c1c-4167ad7a290e"));

            migrationBuilder.UpdateData(
                table: "ItemLists",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 2, 16, 32, 49, 248, DateTimeKind.Unspecified).AddTicks(7727), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ItemLists",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 2, 16, 32, 49, 248, DateTimeKind.Unspecified).AddTicks(7727), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 2, 16, 32, 49, 247, DateTimeKind.Unspecified).AddTicks(7727), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 2, 16, 32, 49, 247, DateTimeKind.Unspecified).AddTicks(7727), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 2, 16, 32, 49, 247, DateTimeKind.Unspecified).AddTicks(7727), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 2, 16, 32, 49, 247, DateTimeKind.Unspecified).AddTicks(7727), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 2, 16, 32, 49, 247, DateTimeKind.Unspecified).AddTicks(7727), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 2, 16, 32, 49, 247, DateTimeKind.Unspecified).AddTicks(7727), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 2, 16, 32, 49, 247, DateTimeKind.Unspecified).AddTicks(7727), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 2, 16, 32, 49, 247, DateTimeKind.Unspecified).AddTicks(7727), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 2, 16, 32, 49, 247, DateTimeKind.Unspecified).AddTicks(7727), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 2, 16, 32, 49, 247, DateTimeKind.Unspecified).AddTicks(7727), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 2, 16, 32, 49, 247, DateTimeKind.Unspecified).AddTicks(7727), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 2, 16, 32, 49, 247, DateTimeKind.Unspecified).AddTicks(7727), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TaskLists",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 2, 16, 32, 49, 248, DateTimeKind.Unspecified).AddTicks(7727), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 2, 16, 32, 49, 249, DateTimeKind.Unspecified).AddTicks(7728), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 2, 16, 32, 49, 249, DateTimeKind.Unspecified).AddTicks(7728), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 2, 16, 32, 49, 249, DateTimeKind.Unspecified).AddTicks(7728), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 2, 16, 32, 49, 249, DateTimeKind.Unspecified).AddTicks(7728), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 2, 16, 32, 49, 249, DateTimeKind.Unspecified).AddTicks(7728), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 2, 16, 32, 49, 241, DateTimeKind.Unspecified).AddTicks(7723), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateCreated", "Email", "PasswordHash" },
                values: new object[,]
                {
                    { new Guid("afa6ea45-c868-401d-b853-e10912ce68c9"), new DateTimeOffset(new DateTime(2020, 9, 2, 16, 32, 49, 249, DateTimeKind.Unspecified).AddTicks(7728), new TimeSpan(0, 3, 0, 0, 0)), "sample3.email@mail.com", "hash_hash" },
                    { new Guid("270faae4-8052-4d37-afa1-235b966b9152"), new DateTimeOffset(new DateTime(2020, 9, 2, 16, 32, 49, 249, DateTimeKind.Unspecified).AddTicks(7728), new TimeSpan(0, 3, 0, 0, 0)), "sample2.email@mail.com", "some_other_hash" },
                    { new Guid("11f47509-040d-40d5-9e69-7eab96f00413"), new DateTimeOffset(new DateTime(2020, 9, 2, 16, 32, 49, 249, DateTimeKind.Unspecified).AddTicks(7728), new TimeSpan(0, 3, 0, 0, 0)), "sample1.email@mail.com", "some_hash" }
                });
        }
    }
}
