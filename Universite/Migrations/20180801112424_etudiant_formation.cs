using Microsoft.EntityFrameworkCore.Migrations;

namespace Universite.Migrations
{
    public partial class etudiant_formation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FormationID",
                table: "Etudiant",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Etudiant_FormationID",
                table: "Etudiant",
                column: "FormationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Etudiant_Formation_FormationID",
                table: "Etudiant",
                column: "FormationID",
                principalTable: "Formation",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Etudiant_Formation_FormationID",
                table: "Etudiant");

            migrationBuilder.DropIndex(
                name: "IX_Etudiant_FormationID",
                table: "Etudiant");

            migrationBuilder.DropColumn(
                name: "FormationID",
                table: "Etudiant");
        }
    }
}
