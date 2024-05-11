using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorDeTarefas.Migrations
{
    /// <inheritdoc />
    public partial class ProjetoCriado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "App");

            migrationBuilder.CreateTable(
                name: "Tarefas",
                schema: "App",
                columns: table => new
                {
                    // Id = table.Column<long>(type: "bigint", nullable: false)
                    //    .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Importancia = table.Column<int>(type: "int", nullable: false),
                    Prazo = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataDoCadastro = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataDaConclusao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tarefas",
                schema: "App");
        }
    }
}
