using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TripFlip.DataAccess.Migrations
{
    public partial class AddApplicationRoleManagementEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "ApplicationRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUsersRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    ApplicationRoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsersRoles", x => new { x.UserId, x.ApplicationRoleId });
                    table.ForeignKey(
                        name: "FK_ApplicationUsersRoles_ApplicationRoles_ApplicationRoleId",
                        column: x => x.ApplicationRoleId,
                        principalTable: "ApplicationRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUsersRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ApplicationRoles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "SuperAdmin" });

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
                    { new Guid("7571d22b-4c41-4ade-8c80-beb91a87fd4a"), new DateTimeOffset(new DateTime(2020, 9, 7, 21, 43, 55, 210, DateTimeKind.Unspecified).AddTicks(9599), new TimeSpan(0, 3, 0, 0, 0)), "sample1.email@mail.com", "some_hash" },
                    { new Guid("b8dbadc4-a1c3-4012-96c7-cce154962375"), new DateTimeOffset(new DateTime(2020, 9, 7, 21, 43, 55, 211, DateTimeKind.Unspecified).AddTicks(127), new TimeSpan(0, 3, 0, 0, 0)), "sample2.email@mail.com", "some_other_hash" },
                    { new Guid("f4053bd9-1fda-40b6-9e53-4e83b2a3feba"), new DateTimeOffset(new DateTime(2020, 9, 7, 21, 43, 55, 211, DateTimeKind.Unspecified).AddTicks(157), new TimeSpan(0, 3, 0, 0, 0)), "sample3.email@mail.com", "hash_hash" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsersRoles_ApplicationRoleId",
                table: "ApplicationUsersRoles",
                column: "ApplicationRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUsersRoles");

            migrationBuilder.DropTable(
                name: "ApplicationRoles");

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
        }
    }
}
