using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Convoquei.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Blabla6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "recorrencias_evento");

            migrationBuilder.CreateIndex(
                name: "IX_recorrencias_evento_data_proxima_geracao",
                table: "recorrencias_evento",
                column: "data_proxima_geracao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_recorrencias_evento_data_proxima_geracao",
                table: "recorrencias_evento");

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "recorrencias_evento",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
