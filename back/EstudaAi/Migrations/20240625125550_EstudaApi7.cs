using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace senaiApiTwo7.Migrations
{
    /// <inheritdoc />
    public partial class EstudaApi7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Estudantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Data_de_Nasc = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Hora_de_Login = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Senha = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudantes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Questaos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Disciplina = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Pergunta = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questaos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Correcaos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RespostaQuestao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DificuldadeQuestao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    questaoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Correcaos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Correcaos_Questaos_questaoId",
                        column: x => x.questaoId,
                        principalTable: "Questaos",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Respostas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RespostaUser = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    questaoId = table.Column<int>(type: "int", nullable: true),
                    estudanteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respostas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Respostas_Estudantes_estudanteId",
                        column: x => x.estudanteId,
                        principalTable: "Estudantes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Respostas_Questaos_questaoId",
                        column: x => x.questaoId,
                        principalTable: "Questaos",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ResultFinals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    respostaId = table.Column<int>(type: "int", nullable: true),
                    correcaoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultFinals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResultFinals_Correcaos_correcaoId",
                        column: x => x.correcaoId,
                        principalTable: "Correcaos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResultFinals_Respostas_respostaId",
                        column: x => x.respostaId,
                        principalTable: "Respostas",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pontuacaos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Acertos = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    resultFinalId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pontuacaos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pontuacaos_ResultFinals_resultFinalId",
                        column: x => x.resultFinalId,
                        principalTable: "ResultFinals",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Registros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    estudanteId = table.Column<int>(type: "int", nullable: true),
                    pontuacaoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registros_Estudantes_estudanteId",
                        column: x => x.estudanteId,
                        principalTable: "Estudantes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Registros_Pontuacaos_pontuacaoId",
                        column: x => x.pontuacaoId,
                        principalTable: "Pontuacaos",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Correcaos_questaoId",
                table: "Correcaos",
                column: "questaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pontuacaos_resultFinalId",
                table: "Pontuacaos",
                column: "resultFinalId");

            migrationBuilder.CreateIndex(
                name: "IX_Registros_estudanteId",
                table: "Registros",
                column: "estudanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Registros_pontuacaoId",
                table: "Registros",
                column: "pontuacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Respostas_estudanteId",
                table: "Respostas",
                column: "estudanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Respostas_questaoId",
                table: "Respostas",
                column: "questaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultFinals_correcaoId",
                table: "ResultFinals",
                column: "correcaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultFinals_respostaId",
                table: "ResultFinals",
                column: "respostaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registros");

            migrationBuilder.DropTable(
                name: "Pontuacaos");

            migrationBuilder.DropTable(
                name: "ResultFinals");

            migrationBuilder.DropTable(
                name: "Correcaos");

            migrationBuilder.DropTable(
                name: "Respostas");

            migrationBuilder.DropTable(
                name: "Estudantes");

            migrationBuilder.DropTable(
                name: "Questaos");
        }
    }
}
