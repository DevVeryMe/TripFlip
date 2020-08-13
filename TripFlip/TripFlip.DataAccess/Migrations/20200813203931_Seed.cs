using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TripFlip.DataAccess.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "DateCreated", "Description", "EndsAt", "StartsAt", "Title" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2020, 8, 13, 23, 39, 28, 584, DateTimeKind.Unspecified).AddTicks(3638), new TimeSpan(0, 3, 0, 0, 0)), "We wanna visit several different cities of Ukraine", new DateTimeOffset(new DateTime(2020, 8, 20, 19, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 8, 17, 14, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Our first trip" });

            migrationBuilder.InsertData(
                table: "Routes",
                columns: new[] { "Id", "DateCreated", "Title", "TripId" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2020, 8, 13, 23, 39, 28, 590, DateTimeKind.Unspecified).AddTicks(8963), new TimeSpan(0, 3, 0, 0, 0)), "First route", 1 });

            migrationBuilder.InsertData(
                table: "Routes",
                columns: new[] { "Id", "DateCreated", "Title", "TripId" },
                values: new object[] { 2, new DateTimeOffset(new DateTime(2020, 8, 13, 23, 39, 28, 590, DateTimeKind.Unspecified).AddTicks(9631), new TimeSpan(0, 3, 0, 0, 0)), "Second route", 1 });

            migrationBuilder.InsertData(
                table: "ItemLists",
                columns: new[] { "Id", "DateCreated", "RouteId", "Title" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2020, 8, 13, 23, 39, 28, 591, DateTimeKind.Unspecified).AddTicks(5652), new TimeSpan(0, 3, 0, 0, 0)), 1, "Most needed items" },
                    { 2, new DateTimeOffset(new DateTime(2020, 8, 13, 23, 39, 28, 591, DateTimeKind.Unspecified).AddTicks(6280), new TimeSpan(0, 3, 0, 0, 0)), 1, "Not needed, but you can take" }
                });

            migrationBuilder.InsertData(
                table: "RoutePoints",
                columns: new[] { "Id", "DateCreated", "Latitude", "Longitude", "Order", "RouteId" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2020, 8, 13, 23, 39, 28, 591, DateTimeKind.Unspecified).AddTicks(2667), new TimeSpan(0, 3, 0, 0, 0)), 56.642000000000003, 14.333, 1, 1 },
                    { 2, new DateTimeOffset(new DateTime(2020, 8, 13, 23, 39, 28, 591, DateTimeKind.Unspecified).AddTicks(3282), new TimeSpan(0, 3, 0, 0, 0)), 60.341000000000001, 17.332000000000001, 2, 1 },
                    { 3, new DateTimeOffset(new DateTime(2020, 8, 13, 23, 39, 28, 591, DateTimeKind.Unspecified).AddTicks(3313), new TimeSpan(0, 3, 0, 0, 0)), 62.622, 18.199000000000002, 3, 1 },
                    { 4, new DateTimeOffset(new DateTime(2020, 8, 13, 23, 39, 28, 591, DateTimeKind.Unspecified).AddTicks(3320), new TimeSpan(0, 3, 0, 0, 0)), 70.849000000000004, 22.143999999999998, 4, 1 },
                    { 5, new DateTimeOffset(new DateTime(2020, 8, 13, 23, 39, 28, 591, DateTimeKind.Unspecified).AddTicks(3327), new TimeSpan(0, 3, 0, 0, 0)), 97.787000000000006, 31.122, 5, 1 },
                    { 6, new DateTimeOffset(new DateTime(2020, 8, 13, 23, 39, 28, 591, DateTimeKind.Unspecified).AddTicks(3334), new TimeSpan(0, 3, 0, 0, 0)), 118.782, 49.523000000000003, 1, 2 },
                    { 7, new DateTimeOffset(new DateTime(2020, 8, 13, 23, 39, 28, 591, DateTimeKind.Unspecified).AddTicks(3341), new TimeSpan(0, 3, 0, 0, 0)), 145.899, 54.320999999999998, 2, 2 },
                    { 8, new DateTimeOffset(new DateTime(2020, 8, 13, 23, 39, 28, 591, DateTimeKind.Unspecified).AddTicks(3349), new TimeSpan(0, 3, 0, 0, 0)), 160.99799999999999, 69.212999999999994, 3, 2 },
                    { 9, new DateTimeOffset(new DateTime(2020, 8, 13, 23, 39, 28, 591, DateTimeKind.Unspecified).AddTicks(3355), new TimeSpan(0, 3, 0, 0, 0)), 180.11099999999999, 71.293999999999997, 4, 2 },
                    { 10, new DateTimeOffset(new DateTime(2020, 8, 13, 23, 39, 28, 591, DateTimeKind.Unspecified).AddTicks(3362), new TimeSpan(0, 3, 0, 0, 0)), 185.23500000000001, 73.224999999999994, 5, 2 }
                });

            migrationBuilder.InsertData(
                table: "TaskLists",
                columns: new[] { "Id", "DateCreated", "RouteId", "Title" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2020, 8, 13, 23, 39, 28, 592, DateTimeKind.Unspecified).AddTicks(839), new TimeSpan(0, 3, 0, 0, 0)), 1, "Tasks" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Comment", "ItemListId", "Quantity", "Title" },
                values: new object[,]
                {
                    { 1, null, 1, null, "Id card" },
                    { 2, null, 1, "1000$", "Money" },
                    { 3, null, 1, null, "Train tickets" },
                    { 4, null, 2, null, "Playing cards" },
                    { 5, null, 2, null, "Food" },
                    { 6, null, 2, null, "Guitar" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "DateCreated", "Description", "TaskListId" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2020, 8, 13, 23, 39, 28, 592, DateTimeKind.Unspecified).AddTicks(2899), new TimeSpan(0, 3, 0, 0, 0)), "Buy food.", 1 },
                    { 2, new DateTimeOffset(new DateTime(2020, 8, 13, 23, 39, 28, 592, DateTimeKind.Unspecified).AddTicks(3944), new TimeSpan(0, 3, 0, 0, 0)), "Buy train tickets", 1 },
                    { 3, new DateTimeOffset(new DateTime(2020, 8, 13, 23, 39, 28, 592, DateTimeKind.Unspecified).AddTicks(3981), new TimeSpan(0, 3, 0, 0, 0)), "Buy tent", 1 },
                    { 4, new DateTimeOffset(new DateTime(2020, 8, 13, 23, 39, 28, 592, DateTimeKind.Unspecified).AddTicks(3989), new TimeSpan(0, 3, 0, 0, 0)), "Buy drugs", 1 },
                    { 5, new DateTimeOffset(new DateTime(2020, 8, 13, 23, 39, 28, 592, DateTimeKind.Unspecified).AddTicks(3995), new TimeSpan(0, 3, 0, 0, 0)), "Buy chips", 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ItemLists",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ItemLists",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TaskLists",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
