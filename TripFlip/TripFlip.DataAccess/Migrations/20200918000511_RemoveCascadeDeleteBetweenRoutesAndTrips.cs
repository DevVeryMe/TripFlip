using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TripFlip.DataAccess.Migrations
{
    public partial class RemoveCascadeDeleteBetweenRoutesAndTrips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Trips_TripId",
                table: "Routes");

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
                value: new DateTimeOffset(new DateTime(2020, 9, 18, 3, 5, 9, 559, DateTimeKind.Unspecified).AddTicks(9407), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ItemLists",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 18, 3, 5, 9, 567, DateTimeKind.Unspecified).AddTicks(1917), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 18, 3, 5, 9, 555, DateTimeKind.Unspecified).AddTicks(6), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 18, 3, 5, 9, 556, DateTimeKind.Unspecified).AddTicks(3746), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 18, 3, 5, 9, 556, DateTimeKind.Unspecified).AddTicks(4581), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 18, 3, 5, 9, 556, DateTimeKind.Unspecified).AddTicks(4715), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 18, 3, 5, 9, 556, DateTimeKind.Unspecified).AddTicks(4740), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 18, 3, 5, 9, 556, DateTimeKind.Unspecified).AddTicks(4760), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 18, 3, 5, 9, 556, DateTimeKind.Unspecified).AddTicks(4782), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 18, 3, 5, 9, 556, DateTimeKind.Unspecified).AddTicks(4803), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 18, 3, 5, 9, 556, DateTimeKind.Unspecified).AddTicks(4823), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 18, 3, 5, 9, 556, DateTimeKind.Unspecified).AddTicks(4841), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 18, 3, 5, 9, 548, DateTimeKind.Unspecified).AddTicks(9466), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 18, 3, 5, 9, 549, DateTimeKind.Unspecified).AddTicks(3160), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TaskLists",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 18, 3, 5, 9, 573, DateTimeKind.Unspecified).AddTicks(1213), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 18, 3, 5, 9, 575, DateTimeKind.Unspecified).AddTicks(5437), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 18, 3, 5, 9, 576, DateTimeKind.Unspecified).AddTicks(1457), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 18, 3, 5, 9, 576, DateTimeKind.Unspecified).AddTicks(1689), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 18, 3, 5, 9, 576, DateTimeKind.Unspecified).AddTicks(1740), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 18, 3, 5, 9, 576, DateTimeKind.Unspecified).AddTicks(1779), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 18, 3, 5, 9, 532, DateTimeKind.Unspecified).AddTicks(1634), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AboutMe", "BirthDate", "DateCreated", "Email", "FirstName", "Gender", "LastName", "PasswordHash" },
                values: new object[,]
                {
                    { new Guid("36894b8b-4c91-4287-8f6f-c60179b0dbe3"), "About me", new DateTimeOffset(new DateTime(2020, 9, 18, 3, 5, 9, 580, DateTimeKind.Unspecified).AddTicks(7555), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 9, 18, 3, 5, 9, 580, DateTimeKind.Unspecified).AddTicks(7522), new TimeSpan(0, 3, 0, 0, 0)), "sample3.email@mail.com", "Stas", 1, "Lazarev", "hash_hash" },
                    { new Guid("a3c3b520-4607-42b2-9334-c5b94d4a5a54"), "About me", new DateTimeOffset(new DateTime(2020, 9, 18, 3, 5, 9, 580, DateTimeKind.Unspecified).AddTicks(7308), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 9, 18, 3, 5, 9, 580, DateTimeKind.Unspecified).AddTicks(6996), new TimeSpan(0, 3, 0, 0, 0)), "sample2.email@mail.com", "Andrew", 1, "Veremiy", "some_other_hash" },
                    { new Guid("20cea857-ca6f-4ea1-907e-9a462146ec2a"), "About me", new DateTimeOffset(new DateTime(2020, 9, 18, 3, 5, 9, 579, DateTimeKind.Unspecified).AddTicks(8025), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 9, 18, 3, 5, 9, 577, DateTimeKind.Unspecified).AddTicks(2693), new TimeSpan(0, 3, 0, 0, 0)), "sample1.email@mail.com", "Andrew", 1, "Kravchuk", "some_hash" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Trips_TripId",
                table: "Routes",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Trips_TripId",
                table: "Routes");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("20cea857-ca6f-4ea1-907e-9a462146ec2a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("36894b8b-4c91-4287-8f6f-c60179b0dbe3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a3c3b520-4607-42b2-9334-c5b94d4a5a54"));

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
                    { new Guid("8f03d642-e4b1-4d34-a3bd-4467ecdfd01b"), "About me", new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 279, DateTimeKind.Unspecified).AddTicks(2048), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 279, DateTimeKind.Unspecified).AddTicks(2041), new TimeSpan(0, 3, 0, 0, 0)), "sample3.email@mail.com", "Stas", 1, "Lazarev", "hash_hash" },
                    { new Guid("f755f10f-85ad-4fa2-9bfe-8d43c8d94aa5"), "About me", new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 279, DateTimeKind.Unspecified).AddTicks(2010), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 279, DateTimeKind.Unspecified).AddTicks(1956), new TimeSpan(0, 3, 0, 0, 0)), "sample2.email@mail.com", "Andrew", 1, "Veremiy", "some_other_hash" },
                    { new Guid("e7cd790b-7dfb-4fb2-bba8-d01a65b39621"), "About me", new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 279, DateTimeKind.Unspecified).AddTicks(1385), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 9, 14, 17, 14, 33, 278, DateTimeKind.Unspecified).AddTicks(8638), new TimeSpan(0, 3, 0, 0, 0)), "sample1.email@mail.com", "Andrew", 1, "Kravchuk", "some_hash" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Trips_TripId",
                table: "Routes",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
