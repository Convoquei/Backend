using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Convoquei.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Blabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "senha",
                table: "usuarios",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_email",
                table: "usuarios",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_usuarios_email",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "senha",
                table: "usuarios");
        }
    }
}
