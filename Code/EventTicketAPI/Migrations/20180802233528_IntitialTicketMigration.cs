using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventTicketAPI.Migrations
{
    public partial class IntitialTicketMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "ticket_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "ticket_type_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "TicketType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    TypeName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(maxLength: 8, nullable: false),
                    EventTitle = table.Column<string>(maxLength: 25, nullable: false),
                    TicketDescription = table.Column<string>(nullable: true),
                    AvailableQty = table.Column<int>(maxLength: 10, nullable: false),
                    TicketPrice = table.Column<decimal>(maxLength: 10, nullable: false),
                    TotalCapacity = table.Column<int>(nullable: false),
                    MinTktsPerOrder = table.Column<int>(maxLength: 5, nullable: false),
                    MaxTktsPerOrder = table.Column<int>(maxLength: 5, nullable: false),
                    SalesStartDate = table.Column<DateTime>(maxLength: 20, nullable: false),
                    SalesEndDate = table.Column<DateTime>(maxLength: 20, nullable: false),
                    TicketTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_TicketType_TicketTypeId",
                        column: x => x.TicketTypeId,
                        principalTable: "TicketType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TicketTypeId",
                table: "Ticket",
                column: "TicketTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "TicketType");

            migrationBuilder.DropSequence(
                name: "ticket_hilo");

            migrationBuilder.DropSequence(
                name: "ticket_type_hilo");
        }
    }
}
