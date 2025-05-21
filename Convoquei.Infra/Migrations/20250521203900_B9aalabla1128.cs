using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Convoquei.Infra.Migrations
{
    /// <inheritdoc />
    public partial class B9aalabla1128 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "data_proxima_geracao",
                table: "recorrencias_evento",
                newName: "data_ultima_geracao");

            migrationBuilder.RenameIndex(
                name: "IX_recorrencias_evento_data_proxima_geracao",
                table: "recorrencias_evento",
                newName: "IX_recorrencias_evento_data_ultima_geracao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "data_ultima_geracao",
                table: "recorrencias_evento",
                newName: "data_proxima_geracao");

            migrationBuilder.RenameIndex(
                name: "IX_recorrencias_evento_data_ultima_geracao",
                table: "recorrencias_evento",
                newName: "IX_recorrencias_evento_data_proxima_geracao");
        }
    }
}
