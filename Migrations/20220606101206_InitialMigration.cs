using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectTestAPI_1.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "stationsFPNozzles",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StationId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    FuelPointId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    NozzleId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    FuelId = table.Column<ulong>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stationsFPNozzles", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "stationsFPNozzles");
        }
    }
}
