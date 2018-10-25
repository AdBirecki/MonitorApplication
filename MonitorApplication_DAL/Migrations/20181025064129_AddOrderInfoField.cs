using Microsoft.EntityFrameworkCore.Migrations;

namespace MonitorApplication_USERS_DAL.Migrations
{
    public partial class AddOrderInfoField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderInfo",
                table: "UserOrders",
                maxLength: 1024,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderInfo",
                table: "UserOrders");
        }
    }
}
