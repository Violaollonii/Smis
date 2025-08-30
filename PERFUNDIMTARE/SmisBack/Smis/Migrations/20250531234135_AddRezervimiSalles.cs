using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smis.Migrations
{
    /// <inheritdoc />
    public partial class AddRezervimiSalles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RezervimiSalles",
                columns: table => new
                {
                    RezervimiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateOnly>(type: "date", nullable: false),
                    Koha = table.Column<TimeOnly>(type: "time", nullable: false),
                    SallaId = table.Column<int>(type: "int", nullable: true),
                    StafiAkademikId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RezervimiSalles", x => x.RezervimiId);
                    table.ForeignKey(
                        name: "FK_RezervimiSalles_Salla_SallaId",
                        column: x => x.SallaId,
                        principalTable: "Salla",
                        principalColumn: "SallaID");
                    table.ForeignKey(
                        name: "FK_RezervimiSalles_StafiAkademik_StafiAkademikId",
                        column: x => x.StafiAkademikId,
                        principalTable: "StafiAkademik",
                        principalColumn: "StafiID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RezervimiSalles_SallaId",
                table: "RezervimiSalles",
                column: "SallaId");

            migrationBuilder.CreateIndex(
                name: "IX_RezervimiSalles_StafiAkademikId",
                table: "RezervimiSalles",
                column: "StafiAkademikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RezervimiSalles");
        }
    }
}
