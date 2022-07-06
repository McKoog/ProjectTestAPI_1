using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectTestAPI_1.Migrations.DiscountCards
{
    public partial class discountcards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "discountCards",
                columns: table => new
                {
                    CardNumber = table.Column<ulong>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CardToken = table.Column<Guid>(type: "TEXT", nullable: false),
                    Balance = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_discountCards", x => x.CardNumber);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "discountCards");
        }
    }
}
