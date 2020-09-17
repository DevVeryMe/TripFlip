using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TripFlip.DataAccess.Migrations
{
    public partial class AddEntitiesforRouteRoleManagement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "RouteRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RouteSubscribers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteId = table.Column<int>(nullable: false),
                    DateSubscribed = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    TripSubscriberId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteSubscribers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteSubscribers_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RouteSubscribers_TripSubscribers_TripSubscriberId",
                        column: x => x.TripSubscriberId,
                        principalTable: "TripSubscribers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RouteSubscribersRoles",
                columns: table => new
                {
                    RouteRoleId = table.Column<int>(nullable: false),
                    RouteSubscriberId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteSubscribersRoles", x => new { x.RouteSubscriberId, x.RouteRoleId });
                    table.ForeignKey(
                        name: "FK_RouteSubscribersRoles_RouteRoles_RouteRoleId",
                        column: x => x.RouteRoleId,
                        principalTable: "RouteRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RouteSubscribersRoles_RouteSubscribers_RouteSubscriberId",
                        column: x => x.RouteSubscriberId,
                        principalTable: "RouteSubscribers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "ItemLists",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 16, 48, 45, 698, DateTimeKind.Unspecified).AddTicks(7894), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ItemLists",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 16, 48, 45, 698, DateTimeKind.Unspecified).AddTicks(7894), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 16, 48, 45, 698, DateTimeKind.Unspecified).AddTicks(7894), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 16, 48, 45, 698, DateTimeKind.Unspecified).AddTicks(7894), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 16, 48, 45, 698, DateTimeKind.Unspecified).AddTicks(7894), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 16, 48, 45, 698, DateTimeKind.Unspecified).AddTicks(7894), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 16, 48, 45, 698, DateTimeKind.Unspecified).AddTicks(7894), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 16, 48, 45, 698, DateTimeKind.Unspecified).AddTicks(7894), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 16, 48, 45, 698, DateTimeKind.Unspecified).AddTicks(7894), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 16, 48, 45, 698, DateTimeKind.Unspecified).AddTicks(7894), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 16, 48, 45, 698, DateTimeKind.Unspecified).AddTicks(7894), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 16, 48, 45, 698, DateTimeKind.Unspecified).AddTicks(7894), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "RouteRoles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Editor" }
                });

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 16, 48, 45, 697, DateTimeKind.Unspecified).AddTicks(7893), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 16, 48, 45, 698, DateTimeKind.Unspecified).AddTicks(7894), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TaskLists",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 16, 48, 45, 699, DateTimeKind.Unspecified).AddTicks(7895), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 16, 48, 45, 699, DateTimeKind.Unspecified).AddTicks(7895), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 16, 48, 45, 699, DateTimeKind.Unspecified).AddTicks(7895), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 16, 48, 45, 699, DateTimeKind.Unspecified).AddTicks(7895), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 16, 48, 45, 699, DateTimeKind.Unspecified).AddTicks(7895), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 16, 48, 45, 699, DateTimeKind.Unspecified).AddTicks(7895), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 9, 17, 16, 48, 45, 691, DateTimeKind.Unspecified).AddTicks(7890), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AboutMe", "BirthDate", "DateCreated", "Email", "FirstName", "Gender", "LastName", "PasswordHash" },
                values: new object[,]
                {
                    { new Guid("fa7f47a6-71f0-416b-bb87-c43af5308a34"), "About me", new DateTimeOffset(new DateTime(2020, 9, 17, 16, 48, 45, 701, DateTimeKind.Unspecified).AddTicks(7896), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 9, 17, 16, 48, 45, 701, DateTimeKind.Unspecified).AddTicks(7896), new TimeSpan(0, 3, 0, 0, 0)), "sample3.email@mail.com", "Stas", 1, "Lazarev", "hash_hash" },
                    { new Guid("3fa2d55f-77b0-49bd-acfe-476470972ea1"), "About me", new DateTimeOffset(new DateTime(2020, 9, 17, 16, 48, 45, 701, DateTimeKind.Unspecified).AddTicks(7896), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 9, 17, 16, 48, 45, 701, DateTimeKind.Unspecified).AddTicks(7896), new TimeSpan(0, 3, 0, 0, 0)), "sample2.email@mail.com", "Andrew", 1, "Veremiy", "some_other_hash" },
                    { new Guid("a4384744-04a0-47d8-87f8-ae3ec555f52b"), "About me", new DateTimeOffset(new DateTime(2020, 9, 17, 16, 48, 45, 701, DateTimeKind.Unspecified).AddTicks(7896), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 9, 17, 16, 48, 45, 700, DateTimeKind.Unspecified).AddTicks(7895), new TimeSpan(0, 3, 0, 0, 0)), "sample1.email@mail.com", "Andrew", 1, "Kravchuk", "some_hash" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RouteSubscribers_RouteId",
                table: "RouteSubscribers",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteSubscribers_TripSubscriberId",
                table: "RouteSubscribers",
                column: "TripSubscriberId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteSubscribersRoles_RouteRoleId",
                table: "RouteSubscribersRoles",
                column: "RouteRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RouteSubscribersRoles");

            migrationBuilder.DropTable(
                name: "RouteRoles");

            migrationBuilder.DropTable(
                name: "RouteSubscribers");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3fa2d55f-77b0-49bd-acfe-476470972ea1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a4384744-04a0-47d8-87f8-ae3ec555f52b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fa7f47a6-71f0-416b-bb87-c43af5308a34"));

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
    }
}
