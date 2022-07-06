using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectTestAPI_1.Migrations.VirtualCards
{
    public partial class virtualcard1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "virtualCards",
                columns: table => new
                {
                    CardNumber = table.Column<ulong>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CardToken = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_virtualCards", x => x.CardNumber);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "virtualCards");
        }
    }
}
