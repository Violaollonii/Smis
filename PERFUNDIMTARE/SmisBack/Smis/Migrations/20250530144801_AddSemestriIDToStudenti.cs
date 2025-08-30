using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smis.Migrations
{
    /// <inheritdoc />
    public partial class AddSemestriIDToStudenti : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Studenti_Semestri_SemestriID",
                table: "Studenti");

            migrationBuilder.AddForeignKey(
                name: "FK_Studenti_Semestri_SemestriID",
                table: "Studenti",
                column: "SemestriID",
                principalTable: "Semestri",
                principalColumn: "SemestriID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Studenti_Semestri_SemestriID",
                table: "Studenti");

            migrationBuilder.AddForeignKey(
                name: "FK_Studenti_Semestri_SemestriID",
                table: "Studenti",
                column: "SemestriID",
                principalTable: "Semestri",
                principalColumn: "SemestriID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
