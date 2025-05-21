using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Convoquei.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Blabla118 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_membros_organizacao_usuarios_UsuarioId",
                table: "membros_organizacao");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "membros_organizacao",
                newName: "usuario_id");

            migrationBuilder.RenameIndex(
                name: "IX_membros_organizacao_UsuarioId",
                table: "membros_organizacao",
                newName: "IX_membros_organizacao_usuario_id");

            migrationBuilder.AddForeignKey(
                name: "FK_membros_organizacao_usuarios_usuario_id",
                table: "membros_organizacao",
                column: "usuario_id",
                principalTable: "usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_membros_organizacao_usuarios_usuario_id",
                table: "membros_organizacao");

            migrationBuilder.RenameColumn(
                name: "usuario_id",
                table: "membros_organizacao",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_membros_organizacao_usuario_id",
                table: "membros_organizacao",
                newName: "IX_membros_organizacao_UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_membros_organizacao_usuarios_UsuarioId",
                table: "membros_organizacao",
                column: "UsuarioId",
                principalTable: "usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
