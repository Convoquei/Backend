
using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.DataTransfer.Usuarios.Response
{
    public record UsuarioResponse(
        Guid Id,
        string Nome,
        string Email
    )
    {
        public static explicit operator UsuarioResponse(Usuario usuario)
        {
            UsuarioResponse response = new(usuario.Id, usuario.Nome, usuario.Email);

            return response;
        }
    }
}
