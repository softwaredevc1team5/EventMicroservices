using Microsoft.EntityFrameworkCore.Migrations;

namespace EventCatalogAPI.Migrations
{
    public partial class addedOrganizerName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Catalog_EventCategory_EventCategoryId",
                table: "Catalog");

            migrationBuilder.DropForeignKey(
                name: "FK_Catalog_EventType_EventTypeId",
                table: "Catalog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Catalog",
                table: "Catalog");

            migrationBuilder.DropSequence(
                name: "catalog_hilo");

            migrationBuilder.RenameTable(
                name: "Catalog",
                newName: "Event");

            migrationBuilder.RenameIndex(
                name: "IX_Catalog_EventTypeId",
                table: "Event",
                newName: "IX_Event_EventTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Catalog_EventCategoryId",
                table: "Event",
                newName: "IX_Event_EventCategoryId");

            migrationBuilder.CreateSequence(
                name: "event_hilo",
                incrementBy: 10);

            migrationBuilder.AddColumn<string>(
                name: "OrganizerName",
                table: "Event",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Event",
                table: "Event",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_EventCategory_EventCategoryId",
                table: "Event",
                column: "EventCategoryId",
                principalTable: "EventCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_EventType_EventTypeId",
                table: "Event",
                column: "EventTypeId",
                principalTable: "EventType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_EventCategory_EventCategoryId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_EventType_EventTypeId",
                table: "Event");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Event",
                table: "Event");

            migrationBuilder.DropSequence(
                name: "event_hilo");

            migrationBuilder.DropColumn(
                name: "OrganizerName",
                table: "Event");

            migrationBuilder.RenameTable(
                name: "Event",
                newName: "Catalog");

            migrationBuilder.RenameIndex(
                name: "IX_Event_EventTypeId",
                table: "Catalog",
                newName: "IX_Catalog_EventTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_EventCategoryId",
                table: "Catalog",
                newName: "IX_Catalog_EventCategoryId");

            migrationBuilder.CreateSequence(
                name: "catalog_hilo",
                incrementBy: 10);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Catalog",
                table: "Catalog",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Catalog_EventCategory_EventCategoryId",
                table: "Catalog",
                column: "EventCategoryId",
                principalTable: "EventCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Catalog_EventType_EventTypeId",
                table: "Catalog",
                column: "EventTypeId",
                principalTable: "EventType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
