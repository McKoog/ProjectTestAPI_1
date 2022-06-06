using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectTestAPI_1.Migrations
{
    public partial class StationConfiguration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_stationsFPNozzles",
                table: "stationsFPNozzles");

            migrationBuilder.RenameTable(
                name: "stationsFPNozzles",
                newName: "stationConfiguration");

            migrationBuilder.AddPrimaryKey(
                name: "PK_stationConfiguration",
                table: "stationConfiguration",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_stationConfiguration",
                table: "stationConfiguration");

            migrationBuilder.RenameTable(
                name: "stationConfiguration",
                newName: "stationsFPNozzles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_stationsFPNozzles",
                table: "stationsFPNozzles",
                column: "Id");
        }
    }
}
