using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Convoquei.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Blabla5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "recorrencia_id",
                table: "eventos",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_eventos_recorrencia_id",
                table: "eventos",
                column: "recorrencia_id");

            migrationBuilder.AddForeignKey(
                name: "FK_eventos_recorrencias_evento_recorrencia_id",
                table: "eventos",
                column: "recorrencia_id",
                principalTable: "recorrencias_evento",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_eventos_recorrencias_evento_recorrencia_id",
                table: "eventos");

            migrationBuilder.DropIndex(
                name: "IX_eventos_recorrencia_id",
                table: "eventos");

            migrationBuilder.DropColumn(
                name: "recorrencia_id",
                table: "eventos");
        }
    }
}
