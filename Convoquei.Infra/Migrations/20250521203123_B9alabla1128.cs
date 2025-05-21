using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Convoquei.Infra.Migrations
{
    /// <inheritdoc />
    public partial class B9alabla1128 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "antecedencia_dias_criar_eventos_recorrentes",
                table: "organizacoes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "antecedencia_dias_criar_eventos_recorrentes",
                table: "organizacoes");
        }
    }
}
