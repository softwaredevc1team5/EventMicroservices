using Microsoft.EntityFrameworkCore.Migrations;

namespace EventCatalogAPI.Migrations
{
    public partial class EventCityMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "event_city_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "EventCity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CityName = table.Column<string>(maxLength: 40, nullable: false),
                    CityDescription = table.Column<string>(maxLength: 500, nullable: false),
                    CityImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCity", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventCity");

            migrationBuilder.DropSequence(
                name: "event_city_hilo");
        }
    }
}
