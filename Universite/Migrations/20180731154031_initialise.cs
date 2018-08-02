using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Universite.Migrations
{
    public partial class initialise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enseignant",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nom = table.Column<string>(nullable: false),
                    Prenom = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enseignant", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Etudiant",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nom = table.Column<string>(nullable: false),
                    Prenom = table.Column<string>(nullable: false),
                    Naissance = table.Column<DateTime>(nullable: false),
                    Genre = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etudiant", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Formation",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IntituleDiplome = table.Column<string>(nullable: false),
                    AnneeDiplome = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formation", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UE",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Numero = table.Column<string>(nullable: false),
                    Intitule = table.Column<string>(nullable: false),
                    FormationID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UE_Formation_FormationID",
                        column: x => x.FormationID,
                        principalTable: "Formation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enseigne",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EnseignantID = table.Column<int>(nullable: false),
                    UEID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enseigne", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Enseigne_Enseignant_EnseignantID",
                        column: x => x.EnseignantID,
                        principalTable: "Enseignant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enseigne_UE_UEID",
                        column: x => x.UEID,
                        principalTable: "UE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Valeur = table.Column<float>(nullable: false),
                    UEID = table.Column<int>(nullable: false),
                    EtudiantID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Note_Etudiant_EtudiantID",
                        column: x => x.EtudiantID,
                        principalTable: "Etudiant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Note_UE_UEID",
                        column: x => x.UEID,
                        principalTable: "UE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enseigne_EnseignantID",
                table: "Enseigne",
                column: "EnseignantID");

            migrationBuilder.CreateIndex(
                name: "IX_Enseigne_UEID",
                table: "Enseigne",
                column: "UEID");

            migrationBuilder.CreateIndex(
                name: "IX_Note_EtudiantID",
                table: "Note",
                column: "EtudiantID");

            migrationBuilder.CreateIndex(
                name: "IX_Note_UEID",
                table: "Note",
                column: "UEID");

            migrationBuilder.CreateIndex(
                name: "IX_UE_FormationID",
                table: "UE",
                column: "FormationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enseigne");

            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropTable(
                name: "Enseignant");

            migrationBuilder.DropTable(
                name: "Etudiant");

            migrationBuilder.DropTable(
                name: "UE");

            migrationBuilder.DropTable(
                name: "Formation");
        }
    }
}
