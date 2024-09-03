using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio_UOL.Migrations
{
    /// <inheritdoc />
    public partial class UOLDDL1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_UOL_GRUPO",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_UOL_GRUPO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_UOL_CODINOME",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    GrupoId = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_UOL_CODINOME", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_UOL_CODINOME_T_UOL_GRUPO_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "T_UOL_GRUPO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_UOL_JOGADOR",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    Telefone = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    CodinomeId = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_UOL_JOGADOR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_UOL_JOGADOR_T_UOL_CODINOME_CodinomeId",
                        column: x => x.CodinomeId,
                        principalTable: "T_UOL_CODINOME",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_UOL_CODINOME_GrupoId",
                table: "T_UOL_CODINOME",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_T_UOL_GRUPO_Nome",
                table: "T_UOL_GRUPO",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_UOL_JOGADOR_CodinomeId",
                table: "T_UOL_JOGADOR",
                column: "CodinomeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_UOL_JOGADOR_Email",
                table: "T_UOL_JOGADOR",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_UOL_JOGADOR");

            migrationBuilder.DropTable(
                name: "T_UOL_CODINOME");

            migrationBuilder.DropTable(
                name: "T_UOL_GRUPO");
        }
    }
}
