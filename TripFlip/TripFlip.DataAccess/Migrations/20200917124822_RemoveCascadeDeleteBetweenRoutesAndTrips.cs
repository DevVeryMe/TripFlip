using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TripFlip.DataAccess.Migrations
{
    public partial class RemoveCascadeDeleteBetweenRoutesAndTrips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("59771c83-7cea-4f17-bcc6-6345b2b765e2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a0012388-e6fd-4bf6-b036-7a9a088df572"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ec3b5fc8-620a-4a94-941d-9a12c9e834b3"));

            migrationBuilder.AlterColumn<int>(
                name: "TripId",
                table: "Routes",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "ItemLists",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 15, 48, 21, 263, DateTimeKind.Unspecified).AddTicks(9741), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ItemLists",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 15, 48, 21, 264, DateTimeKind.Unspecified).AddTicks(407), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 15, 48, 21, 263, DateTimeKind.Unspecified).AddTicks(6461), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 15, 48, 21, 263, DateTimeKind.Unspecified).AddTicks(7139), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 15, 48, 21, 263, DateTimeKind.Unspecified).AddTicks(7172), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 15, 48, 21, 263, DateTimeKind.Unspecified).AddTicks(7182), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 15, 48, 21, 263, DateTimeKind.Unspecified).AddTicks(7188), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 15, 48, 21, 263, DateTimeKind.Unspecified).AddTicks(7195), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 15, 48, 21, 263, DateTimeKind.Unspecified).AddTicks(7201), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 15, 48, 21, 263, DateTimeKind.Unspecified).AddTicks(7208), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 15, 48, 21, 263, DateTimeKind.Unspecified).AddTicks(7214), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 15, 48, 21, 263, DateTimeKind.Unspecified).AddTicks(7220), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 15, 48, 21, 263, DateTimeKind.Unspecified).AddTicks(2296), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 15, 48, 21, 263, DateTimeKind.Unspecified).AddTicks(3031), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TaskLists",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 15, 48, 21, 264, DateTimeKind.Unspecified).AddTicks(5239), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 15, 48, 21, 264, DateTimeKind.Unspecified).AddTicks(7472), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 15, 48, 21, 264, DateTimeKind.Unspecified).AddTicks(8700), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 15, 48, 21, 264, DateTimeKind.Unspecified).AddTicks(8739), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 15, 48, 21, 264, DateTimeKind.Unspecified).AddTicks(8746), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 15, 48, 21, 264, DateTimeKind.Unspecified).AddTicks(8752), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 15, 48, 21, 259, DateTimeKind.Unspecified).AddTicks(6140), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AboutMe", "BirthDate", "DateCreated", "Email", "FirstName", "Gender", "LastName", "PasswordHash" },
                values: new object[,]
                {
                    { new Guid("c26aeac0-b50c-42e2-8642-7040960eba0c"), "About me", new DateTimeOffset(new DateTime(2020, 9, 17, 15, 48, 21, 265, DateTimeKind.Unspecified).AddTicks(5054), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 9, 17, 15, 48, 21, 265, DateTimeKind.Unspecified).AddTicks(5045), new TimeSpan(0, 3, 0, 0, 0)), "sample3.email@mail.com", "Stas", 1, "Lazarev", "hash_hash" },
                    { new Guid("be725055-2cc1-48b2-b2ac-5a482e44cb40"), "About me", new DateTimeOffset(new DateTime(2020, 9, 17, 15, 48, 21, 265, DateTimeKind.Unspecified).AddTicks(4963), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 9, 17, 15, 48, 21, 265, DateTimeKind.Unspecified).AddTicks(4905), new TimeSpan(0, 3, 0, 0, 0)), "sample2.email@mail.com", "Andrew", 1, "Veremiy", "some_other_hash" },
                    { new Guid("15ad6d18-958d-4e94-b086-b0507209659e"), "About me", new DateTimeOffset(new DateTime(2020, 9, 17, 15, 48, 21, 265, DateTimeKind.Unspecified).AddTicks(4254), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 9, 17, 15, 48, 21, 265, DateTimeKind.Unspecified).AddTicks(1082), new TimeSpan(0, 3, 0, 0, 0)), "sample1.email@mail.com", "Andrew", 1, "Kravchuk", "some_hash" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("15ad6d18-958d-4e94-b086-b0507209659e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("be725055-2cc1-48b2-b2ac-5a482e44cb40"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c26aeac0-b50c-42e2-8642-7040960eba0c"));

            migrationBuilder.AlterColumn<int>(
                name: "TripId",
                table: "Routes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "ItemLists",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 998, DateTimeKind.Unspecified).AddTicks(9738), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ItemLists",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 999, DateTimeKind.Unspecified).AddTicks(367), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 998, DateTimeKind.Unspecified).AddTicks(6777), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 998, DateTimeKind.Unspecified).AddTicks(7442), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 998, DateTimeKind.Unspecified).AddTicks(7472), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 998, DateTimeKind.Unspecified).AddTicks(7480), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 998, DateTimeKind.Unspecified).AddTicks(7486), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 998, DateTimeKind.Unspecified).AddTicks(7494), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 998, DateTimeKind.Unspecified).AddTicks(7500), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 998, DateTimeKind.Unspecified).AddTicks(7507), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 998, DateTimeKind.Unspecified).AddTicks(7514), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 998, DateTimeKind.Unspecified).AddTicks(7521), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 998, DateTimeKind.Unspecified).AddTicks(2616), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 998, DateTimeKind.Unspecified).AddTicks(3346), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TaskLists",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 999, DateTimeKind.Unspecified).AddTicks(5447), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 999, DateTimeKind.Unspecified).AddTicks(7704), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 999, DateTimeKind.Unspecified).AddTicks(8803), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 999, DateTimeKind.Unspecified).AddTicks(8841), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 999, DateTimeKind.Unspecified).AddTicks(8848), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 999, DateTimeKind.Unspecified).AddTicks(8854), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 994, DateTimeKind.Unspecified).AddTicks(6074), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AboutMe", "BirthDate", "DateCreated", "Email", "FirstName", "Gender", "LastName", "PasswordHash" },
                values: new object[,]
                {
                    { new Guid("a0012388-e6fd-4bf6-b036-7a9a088df572"), "About me", new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 16, 0, DateTimeKind.Unspecified).AddTicks(5273), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 16, 0, DateTimeKind.Unspecified).AddTicks(5266), new TimeSpan(0, 3, 0, 0, 0)), "sample3.email@mail.com", "Stas", 1, "Lazarev", "hash_hash" },
                    { new Guid("ec3b5fc8-620a-4a94-941d-9a12c9e834b3"), "About me", new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 16, 0, DateTimeKind.Unspecified).AddTicks(5233), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 16, 0, DateTimeKind.Unspecified).AddTicks(5168), new TimeSpan(0, 3, 0, 0, 0)), "sample2.email@mail.com", "Andrew", 1, "Veremiy", "some_other_hash" },
                    { new Guid("59771c83-7cea-4f17-bcc6-6345b2b765e2"), "About me", new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 16, 0, DateTimeKind.Unspecified).AddTicks(4214), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 16, 0, DateTimeKind.Unspecified).AddTicks(1108), new TimeSpan(0, 3, 0, 0, 0)), "sample1.email@mail.com", "Andrew", 1, "Kravchuk", "some_hash" }
                });
        }
    }
}
