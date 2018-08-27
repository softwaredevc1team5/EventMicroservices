using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderApi.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "order_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "ticketorder_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    OrderDate = table.Column<DateTime>(maxLength: 40, nullable: false),
                    UserName = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    PaymentAuthCode = table.Column<string>(nullable: false),
                    OrderTotal = table.Column<decimal>(maxLength: 500, nullable: false),
                    BuyerId = table.Column<string>(nullable: false),
                    StripeToken = table.Column<string>(nullable: false),
                    OrderStatus = table.Column<string>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    EventTitle = table.Column<string>(nullable: false),
                    EventStartDate = table.Column<DateTime>(nullable: false),
                    EventEndDate = table.Column<DateTime>(nullable: false),
                    PictureUrl = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "OrderTicket",
                columns: table => new
                {
                    TicketOrderId = table.Column<int>(nullable: false),
                    TicketTypeId = table.Column<int>(maxLength: 50, nullable: false),
                    TypeName = table.Column<string>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    OrderId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTicket", x => x.TicketOrderId);
                    table.ForeignKey(
                        name: "FK_OrderTicket_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderTicket_Order_OrderId1",
                        column: x => x.OrderId1,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderTicket_OrderId",
                table: "OrderTicket",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTicket_OrderId1",
                table: "OrderTicket",
                column: "OrderId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderTicket");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropSequence(
                name: "order_hilo");

            migrationBuilder.DropSequence(
                name: "ticketorder_hilo");
        }
    }
}
