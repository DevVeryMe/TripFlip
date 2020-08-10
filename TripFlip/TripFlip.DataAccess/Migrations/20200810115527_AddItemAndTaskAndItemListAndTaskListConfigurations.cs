using Microsoft.EntityFrameworkCore.Migrations;

namespace TripFlip.DataAccess.Migrations
{
    public partial class AddItemAndTaskAndItemListAndTaskListConfigurations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaskListId",
                table: "Tasks",
                column: "TaskListId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskLists_RouteId",
                table: "TaskLists",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemListId",
                table: "Items",
                column: "ItemListId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemLists_RouteId",
                table: "ItemLists",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemLists_Routes_RouteId",
                table: "ItemLists",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemLists_ItemListId",
                table: "Items",
                column: "ItemListId",
                principalTable: "ItemLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskLists_Routes_RouteId",
                table: "TaskLists",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskLists_TaskListId",
                table: "Tasks",
                column: "TaskListId",
                principalTable: "TaskLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemLists_Routes_RouteId",
                table: "ItemLists");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemLists_ItemListId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskLists_Routes_RouteId",
                table: "TaskLists");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskLists_TaskListId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_TaskListId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_TaskLists_RouteId",
                table: "TaskLists");

            migrationBuilder.DropIndex(
                name: "IX_Items_ItemListId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_ItemLists_RouteId",
                table: "ItemLists");
        }
    }
}
