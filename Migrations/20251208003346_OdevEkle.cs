using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace odev.dagitim.portali.Migrations
{
    /// <inheritdoc />
    public partial class OdevEkle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Odevler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OgrenciId = table.Column<int>(type: "int", nullable: false),
                    DosyaYolu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YuklemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odevler", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Odevler");
        }
    }
}
