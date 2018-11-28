using Microsoft.EntityFrameworkCore.Migrations;

namespace MonitorApplication_USERS_DAL.Migrations
{
    public partial class MineralPriceDataTypestampChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Timestamp",
                table: "MineralPriceData",
                nullable: false,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Timestamp",
                table: "MineralPriceData",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
