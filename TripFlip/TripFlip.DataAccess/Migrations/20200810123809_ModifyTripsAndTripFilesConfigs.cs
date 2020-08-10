using Microsoft.EntityFrameworkCore.Migrations;

namespace TripFlip.DataAccess.Migrations
{
    public partial class ModifyTripsAndTripFilesConfigs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TripFiles_TripId",
                table: "TripFiles",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_TripFiles_Trips_TripId",
                table: "TripFiles",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripFiles_Trips_TripId",
                table: "TripFiles");

            migrationBuilder.DropIndex(
                name: "IX_TripFiles_TripId",
                table: "TripFiles");
        }
    }
}
