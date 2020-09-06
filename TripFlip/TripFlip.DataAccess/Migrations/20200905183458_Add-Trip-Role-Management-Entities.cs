using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TripFlip.DataAccess.Migrations
{
    public partial class AddTripRoleManagementEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "TripRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TripSubscribers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    TripId = table.Column<int>(nullable: false),
                    DateSubscribed = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripSubscribers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripSubscribers_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripSubscribers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripSubscribersRoles",
                columns: table => new
                {
                    TripSubscriberId = table.Column<int>(nullable: false),
                    TripRoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripSubscribersRoles", x => new { x.TripSubscriberId, x.TripRoleId });
                    table.ForeignKey(
                        name: "FK_TripSubscribersRoles_TripRoles_TripRoleId",
                        column: x => x.TripRoleId,
                        principalTable: "TripRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripSubscribersRoles_TripSubscribers_TripSubscriberId",
                        column: x => x.TripSubscriberId,
                        principalTable: "TripSubscribers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "ItemLists",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 21, 34, 56, 690, DateTimeKind.Unspecified).AddTicks(8449), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ItemLists",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 21, 34, 56, 690, DateTimeKind.Unspecified).AddTicks(8449), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 21, 34, 56, 690, DateTimeKind.Unspecified).AddTicks(8449), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 21, 34, 56, 690, DateTimeKind.Unspecified).AddTicks(8449), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 21, 34, 56, 690, DateTimeKind.Unspecified).AddTicks(8449), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 21, 34, 56, 690, DateTimeKind.Unspecified).AddTicks(8449), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 21, 34, 56, 690, DateTimeKind.Unspecified).AddTicks(8449), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 21, 34, 56, 690, DateTimeKind.Unspecified).AddTicks(8449), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 21, 34, 56, 690, DateTimeKind.Unspecified).AddTicks(8449), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 21, 34, 56, 690, DateTimeKind.Unspecified).AddTicks(8449), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 21, 34, 56, 690, DateTimeKind.Unspecified).AddTicks(8449), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 21, 34, 56, 690, DateTimeKind.Unspecified).AddTicks(8449), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 21, 34, 56, 689, DateTimeKind.Unspecified).AddTicks(8449), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 21, 34, 56, 689, DateTimeKind.Unspecified).AddTicks(8449), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TaskLists",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 21, 34, 56, 691, DateTimeKind.Unspecified).AddTicks(8450), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 21, 34, 56, 691, DateTimeKind.Unspecified).AddTicks(8450), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 21, 34, 56, 691, DateTimeKind.Unspecified).AddTicks(8450), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 21, 34, 56, 691, DateTimeKind.Unspecified).AddTicks(8450), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 21, 34, 56, 691, DateTimeKind.Unspecified).AddTicks(8450), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 21, 34, 56, 691, DateTimeKind.Unspecified).AddTicks(8450), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "TripRoles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 3, "Guest" },
                    { 2, "Editor" },
                    { 1, "Admin" }
                });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 5, 21, 34, 56, 683, DateTimeKind.Unspecified).AddTicks(8445), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateCreated", "Email", "PasswordHash" },
                values: new object[,]
                {
                    { new Guid("6c919abc-5845-49e1-ab97-8bad7b19b154"), new DateTimeOffset(new DateTime(2020, 9, 5, 21, 34, 56, 691, DateTimeKind.Unspecified).AddTicks(8450), new TimeSpan(0, 3, 0, 0, 0)), "sample3.email@mail.com", "hash_hash" },
                    { new Guid("8bcc1e77-d183-41c9-8210-038d00ab12e4"), new DateTimeOffset(new DateTime(2020, 9, 5, 21, 34, 56, 691, DateTimeKind.Unspecified).AddTicks(8450), new TimeSpan(0, 3, 0, 0, 0)), "sample2.email@mail.com", "some_other_hash" },
                    { new Guid("f7b70c34-27d3-4e9a-b64c-5fb0bad49ee9"), new DateTimeOffset(new DateTime(2020, 9, 5, 21, 34, 56, 691, DateTimeKind.Unspecified).AddTicks(8450), new TimeSpan(0, 3, 0, 0, 0)), "sample1.email@mail.com", "some_hash" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TripSubscribers_TripId",
                table: "TripSubscribers",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_TripSubscribers_UserId",
                table: "TripSubscribers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TripSubscribersRoles_TripRoleId",
                table: "TripSubscribersRoles",
                column: "TripRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripSubscribersRoles");

            migrationBuilder.DropTable(
                name: "TripRoles");

            migrationBuilder.DropTable(
                name: "TripSubscribers");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6c919abc-5845-49e1-ab97-8bad7b19b154"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8bcc1e77-d183-41c9-8210-038d00ab12e4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f7b70c34-27d3-4e9a-b64c-5fb0bad49ee9"));

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
        }
    }
}
