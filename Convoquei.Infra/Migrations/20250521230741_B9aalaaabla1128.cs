using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Convoquei.Infra.Migrations
{
    /// <inheritdoc />
    public partial class B9aalaaabla1128 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_recorrencias_evento_data_ultima_geracao",
                table: "recorrencias_evento");

            migrationBuilder.RenameColumn(
                name: "data_ultima_geracao",
                table: "recorrencias_evento",
                newName: "primeira_ocorrencia");

            migrationBuilder.RenameColumn(
                name: "data_hora_inicio",
                table: "recorrencias_evento",
                newName: "previsao_proxima_geracao");

            migrationBuilder.AddColumn<DateTime>(
                name: "data_ultimo_evento_gerado",
                table: "recorrencias_evento",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "horario_inicio",
                table: "recorrencias_evento",
                type: "interval",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_recorrencias_evento_data_ultimo_evento_gerado",
                table: "recorrencias_evento",
                column: "data_ultimo_evento_gerado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_recorrencias_evento_data_ultimo_evento_gerado",
                table: "recorrencias_evento");

            migrationBuilder.DropColumn(
                name: "data_ultimo_evento_gerado",
                table: "recorrencias_evento");

            migrationBuilder.DropColumn(
                name: "horario_inicio",
                table: "recorrencias_evento");

            migrationBuilder.RenameColumn(
                name: "primeira_ocorrencia",
                table: "recorrencias_evento",
                newName: "data_ultima_geracao");

            migrationBuilder.RenameColumn(
                name: "previsao_proxima_geracao",
                table: "recorrencias_evento",
                newName: "data_hora_inicio");

            migrationBuilder.CreateIndex(
                name: "IX_recorrencias_evento_data_ultima_geracao",
                table: "recorrencias_evento",
                column: "data_ultima_geracao");
        }
    }
}
