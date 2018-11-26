using Microsoft.EntityFrameworkCore.Migrations;

namespace Whoops.Migrations
{
    public partial class ColumsNamingCorrected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Logntitude",
                table: "Stops",
                newName: "Longtitude");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Stops",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Stops");

            migrationBuilder.RenameColumn(
                name: "Longtitude",
                table: "Stops",
                newName: "Logntitude");
        }
    }
}
