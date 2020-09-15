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
                keyValue: new Guid("0c5d006c-3ccb-4ce3-970c-0c7a3e7c9474"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4bf56251-0351-45b1-bcc9-c4231269e3f2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("79bca920-629f-4a9c-85f1-493fe9563f9c"));

            migrationBuilder.InsertData(
                table: "ApplicationRoles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Admin" });

            migrationBuilder.UpdateData(
                table: "ItemLists",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 277, DateTimeKind.Unspecified).AddTicks(8808), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ItemLists",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 277, DateTimeKind.Unspecified).AddTicks(9371), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 277, DateTimeKind.Unspecified).AddTicks(6187), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 277, DateTimeKind.Unspecified).AddTicks(6767), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 277, DateTimeKind.Unspecified).AddTicks(6793), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 277, DateTimeKind.Unspecified).AddTicks(6800), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 277, DateTimeKind.Unspecified).AddTicks(6806), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 277, DateTimeKind.Unspecified).AddTicks(6858), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 277, DateTimeKind.Unspecified).AddTicks(6866), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 277, DateTimeKind.Unspecified).AddTicks(6872), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 277, DateTimeKind.Unspecified).AddTicks(6878), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 277, DateTimeKind.Unspecified).AddTicks(6884), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 277, DateTimeKind.Unspecified).AddTicks(2759), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 277, DateTimeKind.Unspecified).AddTicks(3387), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TaskLists",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 278, DateTimeKind.Unspecified).AddTicks(3559), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 278, DateTimeKind.Unspecified).AddTicks(5556), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 278, DateTimeKind.Unspecified).AddTicks(6605), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 278, DateTimeKind.Unspecified).AddTicks(6642), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 278, DateTimeKind.Unspecified).AddTicks(6649), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 278, DateTimeKind.Unspecified).AddTicks(6655), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 274, DateTimeKind.Unspecified).AddTicks(476), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AboutMe", "BirthDate", "DateCreated", "Email", "FirstName", "Gender", "LastName", "PasswordHash" },
                values: new object[,]
                {
                    { new Guid("e7cd790b-7dfb-4fb2-bba8-d01a65b39621"), "About me", new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 279, DateTimeKind.Unspecified).AddTicks(1385), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 278, DateTimeKind.Unspecified).AddTicks(8638), new TimeSpan(0, 3, 0, 0, 0)), "sample1.email@mail.com", "Andrew", 1, "Kravchuk", "some_hash" },
                    { new Guid("f755f10f-85ad-4fa2-9bfe-8d43c8d94aa5"), "About me", new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 279, DateTimeKind.Unspecified).AddTicks(2010), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 279, DateTimeKind.Unspecified).AddTicks(1956), new TimeSpan(0, 3, 0, 0, 0)), "sample2.email@mail.com", "Andrew", 1, "Veremiy", "some_other_hash" },
                    { new Guid("8f03d642-e4b1-4d34-a3bd-4467ecdfd01b"), "About me", new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 279, DateTimeKind.Unspecified).AddTicks(2048), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 279, DateTimeKind.Unspecified).AddTicks(2041), new TimeSpan(0, 3, 0, 0, 0)), "sample3.email@mail.com", "Stas", 1, "Lazarev", "hash_hash" }
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
                keyValue: new Guid("8f03d642-e4b1-4d34-a3bd-4467ecdfd01b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e7cd790b-7dfb-4fb2-bba8-d01a65b39621"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f755f10f-85ad-4fa2-9bfe-8d43c8d94aa5"));

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
    }
}
