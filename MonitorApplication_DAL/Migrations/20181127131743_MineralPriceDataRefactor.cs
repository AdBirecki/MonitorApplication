using Microsoft.EntityFrameworkCore.Migrations;

namespace MonitorApplication_USERS_DAL.Migrations
{
    public partial class MineralPriceDataRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "timestamp",
                table: "MineralPriceData",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "dateTime",
                table: "MineralPriceData",
                newName: "TimeOfRegistration");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "MineralPriceData",
                newName: "timestamp");

            migrationBuilder.RenameColumn(
                name: "TimeOfRegistration",
                table: "MineralPriceData",
                newName: "dateTime");
        }
    }
}
