using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WishListAPI.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "WishCartItem_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "WishCartItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    BuyerId = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    EventTitle = table.Column<string>(maxLength: 50, nullable: false),
                    NumOfTickets = table.Column<int>(nullable: false),
                    TicketPrice = table.Column<decimal>(nullable: false),
                    TicketType = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishCartItem", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WishCartItems");

            migrationBuilder.DropSequence(
                name: "WishCartItem_hilo");
        }
    }
}
