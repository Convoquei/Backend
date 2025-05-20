using Convoquei.Core.Usuarios.Entidades;
using Convoquei.Core.Usuarios.ValueObjects;

namespace Convoquei.DataTransfer.Usuarios.Response
{
    public record AutenticadoResponse(
        TokenRefreshResponse Tokens,
        UsuarioResponse Usuario
    )
    {
        public static explicit operator AutenticadoResponse(Usuario usuario)
        {
            TokenRefreshResponse tokenRefreshResponse = (TokenRefreshResponse)usuario.Token!;
            UsuarioResponse usuarioResponse = (UsuarioResponse)usuario;

            return new AutenticadoResponse(tokenRefreshResponse, usuarioResponse);
        }
    }

    public record TokenRefreshResponse(
        string Token,
        string RefreshToken
    )
    {
        public static explicit operator TokenRefreshResponse(Token token)
        {
            return new TokenRefreshResponse(token.Acesso, token.Refresh);
        }
    }
}
