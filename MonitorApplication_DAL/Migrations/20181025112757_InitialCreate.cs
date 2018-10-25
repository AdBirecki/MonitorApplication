using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MonitorApplication_USERS_DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MineralPriceData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Currency = table.Column<int>(nullable: false),
                    GoldPrice = table.Column<double>(nullable: false),
                    SilverPrice = table.Column<double>(nullable: false),
                    GoldChange = table.Column<double>(nullable: false),
                    SilverChange = table.Column<double>(nullable: false),
                    GoldPcExchange = table.Column<double>(nullable: false),
                    SilverPcExchange = table.Column<double>(nullable: false),
                    GoldClose = table.Column<double>(nullable: false),
                    SilverClose = table.Column<double>(nullable: false),
                    timestamp = table.Column<long>(nullable: false),
                    dateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MineralPriceData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => new { x.UserId, x.Username });
                });

            migrationBuilder.CreateTable(
                name: "UserOrders",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    OrderInfo = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOrders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_UserOrders_Users_UserId_Username",
                        columns: x => new { x.UserId, x.Username },
                        principalTable: "Users",
                        principalColumns: new[] { "UserId", "Username" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    PurchaseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserOrdersOrderId = table.Column<int>(nullable: true),
                    dateTime = table.Column<DateTime>(nullable: false),
                    TimeStamp = table.Column<long>(nullable: false),
                    Info = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.PurchaseId);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_UserOrders_UserOrdersOrderId",
                        column: x => x.UserOrdersOrderId,
                        principalTable: "UserOrders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AbstractOrders",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false),
                    Currencies = table.Column<int>(nullable: false),
                    Units = table.Column<int>(nullable: false),
                    PurchaseOrderPurchaseId = table.Column<int>(nullable: true),
                    PricePerUnit = table.Column<double>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    MineralType = table.Column<int>(nullable: false),
                    PalladiumFiness = table.Column<double>(nullable: true),
                    GoldFiness = table.Column<double>(nullable: true),
                    Carat = table.Column<int>(nullable: true),
                    SilverOrder_GoldFiness = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbstractOrders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_AbstractOrders_MineralPriceData_OrderId",
                        column: x => x.OrderId,
                        principalTable: "MineralPriceData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbstractOrders_PurchaseOrders_PurchaseOrderPurchaseId",
                        column: x => x.PurchaseOrderPurchaseId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbstractOrders_PurchaseOrderPurchaseId",
                table: "AbstractOrders",
                column: "PurchaseOrderPurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_UserOrdersOrderId",
                table: "PurchaseOrders",
                column: "UserOrdersOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrders_UserId_Username",
                table: "UserOrders",
                columns: new[] { "UserId", "Username" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbstractOrders");

            migrationBuilder.DropTable(
                name: "MineralPriceData");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "UserOrders");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
