using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Confere.Telefonia.Web.Migrations
{
    public partial class Ligacoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcionarios_Setores_SetorId",
                table: "Funcionarios");

            migrationBuilder.AlterColumn<int>(
                name: "SetorId",
                table: "Funcionarios",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Ligacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Contato = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DestinoId = table.Column<int>(type: "int", nullable: false),
                    Duracao = table.Column<TimeSpan>(type: "time", nullable: false),
                    FuncionarioId = table.Column<int>(type: "int", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Particular = table.Column<bool>(type: "bit", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelefonistaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ligacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ligacoes_Destinos_DestinoId",
                        column: x => x.DestinoId,
                        principalTable: "Destinos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ligacoes_Funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ligacoes_Telefonistas_TelefonistaId",
                        column: x => x.TelefonistaId,
                        principalTable: "Telefonistas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ligacoes_DestinoId",
                table: "Ligacoes",
                column: "DestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ligacoes_FuncionarioId",
                table: "Ligacoes",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Ligacoes_TelefonistaId",
                table: "Ligacoes",
                column: "TelefonistaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionarios_Setores_SetorId",
                table: "Funcionarios",
                column: "SetorId",
                principalTable: "Setores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcionarios_Setores_SetorId",
                table: "Funcionarios");

            migrationBuilder.DropTable(
                name: "Ligacoes");

            migrationBuilder.AlterColumn<int>(
                name: "SetorId",
                table: "Funcionarios",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionarios_Setores_SetorId",
                table: "Funcionarios",
                column: "SetorId",
                principalTable: "Setores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
