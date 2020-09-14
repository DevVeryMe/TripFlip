using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TripFlip.DataAccess.Migrations
{
    public partial class Extenduserentity : Migration
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

            migrationBuilder.AddColumn<string>(
                name: "AboutMe",
                table: "Users",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "BirthDate",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ItemLists",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 10, 19, 8, 4, 231, DateTimeKind.Unspecified).AddTicks(3196), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ItemLists",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 10, 19, 8, 4, 231, DateTimeKind.Unspecified).AddTicks(4572), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 10, 19, 8, 4, 230, DateTimeKind.Unspecified).AddTicks(6756), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 10, 19, 8, 4, 230, DateTimeKind.Unspecified).AddTicks(8181), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 10, 19, 8, 4, 230, DateTimeKind.Unspecified).AddTicks(8250), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 10, 19, 8, 4, 230, DateTimeKind.Unspecified).AddTicks(8264), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 10, 19, 8, 4, 230, DateTimeKind.Unspecified).AddTicks(8277), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 10, 19, 8, 4, 230, DateTimeKind.Unspecified).AddTicks(8291), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 10, 19, 8, 4, 230, DateTimeKind.Unspecified).AddTicks(8305), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 10, 19, 8, 4, 230, DateTimeKind.Unspecified).AddTicks(8318), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 10, 19, 8, 4, 230, DateTimeKind.Unspecified).AddTicks(8331), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 10, 19, 8, 4, 230, DateTimeKind.Unspecified).AddTicks(8343), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 10, 19, 8, 4, 229, DateTimeKind.Unspecified).AddTicks(8144), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 10, 19, 8, 4, 229, DateTimeKind.Unspecified).AddTicks(9832), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TaskLists",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 10, 19, 8, 4, 232, DateTimeKind.Unspecified).AddTicks(5797), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 10, 19, 8, 4, 233, DateTimeKind.Unspecified).AddTicks(862), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 10, 19, 8, 4, 233, DateTimeKind.Unspecified).AddTicks(3409), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 10, 19, 8, 4, 233, DateTimeKind.Unspecified).AddTicks(3492), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 10, 19, 8, 4, 233, DateTimeKind.Unspecified).AddTicks(3507), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 10, 19, 8, 4, 233, DateTimeKind.Unspecified).AddTicks(3521), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 10, 19, 8, 4, 222, DateTimeKind.Unspecified).AddTicks(3312), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AboutMe", "BirthDate", "DateCreated", "Email", "FirstName", "Gender", "LastName", "PasswordHash" },
                values: new object[,]
                {
                    { new Guid("79bca920-629f-4a9c-85f1-493fe9563f9c"), "About me", new DateTimeOffset(new DateTime(2020, 9, 10, 19, 8, 4, 234, DateTimeKind.Unspecified).AddTicks(7067), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 9, 10, 19, 8, 4, 234, DateTimeKind.Unspecified).AddTicks(7052), new TimeSpan(0, 3, 0, 0, 0)), "sample3.email@mail.com", "Stas", 1, "Lazarev", "hash_hash" },
                    { new Guid("0c5d006c-3ccb-4ce3-970c-0c7a3e7c9474"), "About me", new DateTimeOffset(new DateTime(2020, 9, 10, 19, 8, 4, 234, DateTimeKind.Unspecified).AddTicks(6984), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 9, 10, 19, 8, 4, 234, DateTimeKind.Unspecified).AddTicks(6855), new TimeSpan(0, 3, 0, 0, 0)), "sample2.email@mail.com", "Andrew", 1, "Veremiy", "some_other_hash" },
                    { new Guid("4bf56251-0351-45b1-bcc9-c4231269e3f2"), "About me", new DateTimeOffset(new DateTime(2020, 9, 10, 19, 8, 4, 234, DateTimeKind.Unspecified).AddTicks(5452), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 9, 10, 19, 8, 4, 233, DateTimeKind.Unspecified).AddTicks(8626), new TimeSpan(0, 3, 0, 0, 0)), "sample1.email@mail.com", "Andrew", 1, "Kravchuk", "some_hash" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0c5d006c-3ccb-4ce3-970c-0c7a3e7c9474"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4bf56251-0351-45b1-bcc9-c4231269e3f2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("79bca920-629f-4a9c-85f1-493fe9563f9c"));

            migrationBuilder.DropColumn(
                name: "AboutMe",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

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
